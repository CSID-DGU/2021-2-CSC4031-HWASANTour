using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;
using System.ComponentModel;
using System;
using UnityEngine.UI;
using TMPro;

/** On Metaverse Scene */

public class DguMetaverse : MonoBehaviour
{
    #region Enums

    /// <summary>
    /// Defines properties that can change.  Used by the functions that subscribe to the OnAfterTYPEValueUpdated functions.
    /// </summary>
    public enum ChangedProperty
    {
        None,
        Speaking,
        Typing,
        Muted
    }

    public enum ChatCapability
    {
        TextOnly,
        AudioOnly,
        TextAndAudio
    };

    #endregion


    private Uri _serverUri
    {
        get => new Uri(_server);

        set
        {
            _server = value.ToString();
        }
    }
    [SerializeField]
    private string _server = "https://mt1s.www.vivox.com/api2";
    [SerializeField]
    private string _domain = "mt1s.vivox.com";
    [SerializeField]
    private string _tokenIssuer = "taekwo4405-me58-dev";
    [SerializeField]
    private string _tokenKey = "chem747";

    private TimeSpan _tokenExpiration = TimeSpan.FromSeconds(90);

    public LoginState LoginState { get; private set; }
    public ILoginSession LoginSession;
    public VivoxUnity.IReadOnlyDictionary<ChannelId, IChannelSession> ActiveChannels => LoginSession?.ChannelSessions;

    private ChannelId _channelId;
    private AccountId _accountId;

    private Client _client = new Client();

    #region UI Variables


    [SerializeField]
    public Button GroupChat_Button_SEND;
    [SerializeField]
    public Text GroupChat_Text;
    [SerializeField]
    public InputField GroupChat_InputField_MSG;
    [SerializeField]
    public ScrollRect GroupChat_Scroll;
    [SerializeField]
    public Text GroupChat_Channel;

    #endregion

    #region Delegates/Events
    // Participants Delegate & Event 
    public delegate void ParticipantStatusChangedHandler(string username, ChannelId channel, IParticipant participant);
    public event ParticipantStatusChangedHandler OnParticipantAddedEvent;
    public event ParticipantStatusChangedHandler OnParticipantRemovedEvent;

    // Message Delegate & Event 
    public delegate void ChannelTextMessageChangedHandler(string sender, IChannelTextMessage channelTextMessage);
    //public event ChannelTextMessageChangedHandler OnTextMessageLogReceivedEvent;

    // LoginStatus -> LoginStatusChangedHandler (Delegate) -> OnUserLoggedInEvent, OnUserLoggedOutEvent  
    public delegate void LoginStatusChangedHandler();
    public event LoginStatusChangedHandler OnUserLoggedInEvent;
    //public event LoginStatusChangedHandler OnUserLoggedOutEvent;

    #endregion

    private void Awake()
    {
        VivoxLog("Awake() is called");
        _client.Uninitialize();

        _client.Initialize();

        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        VivoxLog("OnApplicationQuit() is called");
        _client.Uninitialize();
        _client = null;
    }

    void Start()
    {
        VivoxLog("Start() is called");
        GroupChat_Channel.text = LoginManager.LoginUserMajor;
        GroupChat_Button_SEND.interactable = false;
        // Make it Variable from component
        GroupChat_Button_SEND.onClick.AddListener(() => SendTextMessage(LoginManager.LoginUserNickname + " : " + GroupChat_InputField_MSG.text));

        string uniqueId = Guid.NewGuid().ToString();
        VivoxLog("uniqueId = " + uniqueId);
        //for proto purposes only, need to get a real token from server eventually
        _accountId = new AccountId(_tokenIssuer, uniqueId, _domain, null);
        LoginSession = _client.GetLoginSession(_accountId);
        LoginSession.PropertyChanged += OnLoginSessionPropertyChanged;
        LoginSession.BeginLogin(_serverUri, LoginSession.GetLoginToken(_tokenKey, _tokenExpiration), SubscriptionMode.Accept, null, null, null, ar =>
        {
            try
            {
                VivoxLog("_accountId = " + _accountId);
                /** Login Success Callback */
                VivoxLog("로그인 유저 전공(=가입 채널) = " + LoginManager.LoginUserMajor);
                GroupChat_Text.text += "<color=#00d49c>" + "[sys] " + LoginManager.LoginUserMajor + "학과 채널에 입장하셨습니다." + "</color>";
                JoinChannel(LoginManager.LoginUserMajor, ChatCapability.TextOnly);
                LoginSession.EndLogin(ar);
            }
            catch (Exception e)
            {
                // Handle error 
                Debug.Log("Login Error");
                Debug.Log(e.Message);
                LoginSession.PropertyChanged -= OnLoginSessionPropertyChanged;
                return;
            }
        });
    }


    /** 
     *   Vivox Server Disconnect 
     */
    public void Logout()
    {
        if (LoginSession != null && LoginState != LoginState.LoggedOut && LoginState != LoginState.LoggingOut)
        {
            LoginSession.Logout();
            LoginSession.PropertyChanged -= OnLoginSessionPropertyChanged;
        }
    }

    /** 
     *   Create Group Channels needed (on start up)
     *   
     ChatCapability
     *   - TextOnly 
     *   - AudioOnly
     *   - TextAndAudio
     *
     ChannelType 
     *   - NonPositional
     *   - Positional
     *   - Echo
     */
    public void JoinChannel(string channelName, ChatCapability chatCapability,
        bool switchTransmission = true, Channel3DProperties properties = null)
    {
        VivoxLog("LoginState = " + LoginState);
        if (LoginState == LoginState.LoggedIn)
        {
            ChannelId channelId = new ChannelId(_tokenIssuer, channelName, _domain, ChannelType.NonPositional, properties);
            _channelId = channelId;
            IChannelSession channelSession = LoginSession.GetChannelSession(channelId);

            channelSession.PropertyChanged += OnChannelPropertyChanged;
            channelSession.Participants.AfterKeyAdded += OnParticipantAdded;
            channelSession.Participants.BeforeKeyRemoved += OnParticipantRemoved;
            channelSession.MessageLog.AfterItemAdded += OnMessageLogRecieved;

            channelSession.BeginConnect(false, true, switchTransmission, channelSession.GetConnectToken(_tokenKey, _tokenExpiration), ar =>
            {
                try
                {
                    VivoxLog("[JoinChannel] = " + channelName);
                    channelSession.EndConnect(ar);
                    GroupChat_Button_SEND.interactable = true;
                }
                catch (Exception e)
                {
                    // Handle error 
                    VivoxLogError($"Could not connect to voice channel: {e.Message}");
                    return;
                }
            });
        }
        else
        {
            VivoxLogError("Cannot join a channel when not logged in.");
        }
    }


    public void SendTextMessage(string messageToSend)
    {
        GroupChat_InputField_MSG.text = "";

        if (ChannelId.IsNullOrEmpty(_channelId))
        {
            throw new ArgumentException("Must provide a valid ChannelId");
        }
        if (string.IsNullOrEmpty(messageToSend))
        {
            throw new ArgumentException("Must provide a message to send");
        }
        var channelSession = LoginSession.GetChannelSession(_channelId);
        channelSession.BeginSendText(null, messageToSend, null, null, ar =>
        {
            try
            {
                VivoxLog("[SendTextMessage] MSG = " + messageToSend);
                channelSession.EndSendText(ar);
            }
            catch (Exception e)
            {
                VivoxLog($"SendTextMessage failed with exception {e.Message}");
            }
        });
    }
    #region Vivox Callbacks

    private void OnMessageLogRecieved(object sender, QueueItemAddedEventArgs<IChannelTextMessage> textMessage)
    {
        ValidateArgs(new object[] { sender, textMessage });
        IChannelTextMessage channelTextMessage = textMessage.Value;

        VivoxLog("[OnMessageLogRecieved] channelTextMessage.Sender.DisplayName = " + channelTextMessage.Sender.DisplayName);
        VivoxLog("[OnMessageLogRecieved] channelTextMessage.Message = " + channelTextMessage.Message);

        if (channelTextMessage.FromSelf)
        {
            VivoxLog("<color=#ff5c3e>" + "My Message" + "</color>");
            GroupChat_Text.text += "\n" + "<color=#ff5c3e>" + channelTextMessage.Message + "</color>";
        }
        else
        {
            VivoxLog("Not My Message");
            GroupChat_Text.text += "\n" + channelTextMessage.Message;
        }
    }



    // 이벤트를 발생시키는 개체를 = event sender 
    private void OnLoginSessionPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
    {
        if (propertyChangedEventArgs.PropertyName != "State")
        {
            return;
        }
        var loginSession = (ILoginSession)sender;
        LoginState = loginSession.State;
        Debug.Log("LoginState = " + LoginState);
        VivoxLog("Detecting login session change");
        switch (LoginState)
        {
            case LoginState.LoggingIn:
                {
                    VivoxLog("Logging in");
                    break;
                }
            case LoginState.LoggedIn:
                {
                    VivoxLog("Connected to voice server and logged in.");
                    OnUserLoggedInEvent?.Invoke();
                    break;
                }
            case LoginState.LoggingOut:
                {
                    VivoxLog("Logging out");
                    break;
                }
            case LoginState.LoggedOut:
                {
                    VivoxLog("Logged out");
                    LoginSession.PropertyChanged -= OnLoginSessionPropertyChanged;
                    break;
                }
            default:
                break;
        }
    }

    private void OnChannelPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
    {
        ValidateArgs(new object[] { sender, propertyChangedEventArgs });

        //if (_client == null)
        //    throw new InvalidClient("Invalid client.");
        var channelSession = (IChannelSession)sender;

        // IF the channel has fully disconnected, unsubscribe and remove.
        if ((propertyChangedEventArgs.PropertyName == "AudioState" || propertyChangedEventArgs.PropertyName == "TextState") &&
            channelSession.AudioState == ConnectionState.Disconnected &&
            channelSession.TextState == ConnectionState.Disconnected)
        {
            VivoxLog($"Unsubscribing from: {channelSession.Key.Name}");
            // Now that we are disconnected, unsubscribe.
            channelSession.PropertyChanged -= OnChannelPropertyChanged;
            channelSession.Participants.AfterKeyAdded -= OnParticipantAdded;
            channelSession.Participants.BeforeKeyRemoved -= OnParticipantRemoved;
            channelSession.MessageLog.AfterItemAdded -= OnMessageLogRecieved;

            // Remove session.
            var user = _client.GetLoginSession(_accountId);
            user.DeleteChannelSession(channelSession.Channel);

        }
    }

    private void OnParticipantAdded(object sender, KeyEventArg<string> keyEventArg)
    {
        ValidateArgs(new object[] { sender, keyEventArg });

        // INFO: sender is the dictionary that changed and trigger the event.  Need to cast it back to access it.
        var source = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;
        // Look up the participant via the key.
        var participant = source[keyEventArg.Key];
        var username = participant.Account.Name;
        var channel = participant.ParentChannelSession.Key;
        var channelSession = participant.ParentChannelSession;

        // username = unique id 
        VivoxLog("[OnParticipantAdded] username = " + username);
        VivoxLog("[OnParticipantAdded] channel = " + channel);


        // Trigger callback
        OnParticipantAddedEvent?.Invoke(username, channel, participant);
    }

    private void OnParticipantRemoved(object sender, KeyEventArg<string> keyEventArg)
    {
        ValidateArgs(new object[] { sender, keyEventArg });

        // INFO: sender is the dictionary that changed and trigger the event.  Need to cast it back to access it.
        var source = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;
        // Look up the participant via the key.
        var participant = source[keyEventArg.Key];
        var username = participant.Account.Name;
        var channel = participant.ParentChannelSession.Key;
        var channelSession = participant.ParentChannelSession;

        if (participant.IsSelf)
        {
            VivoxLog($"Unsubscribing from: {channelSession.Key.Name}");
            // Now that we are disconnected, unsubscribe.
            channelSession.Participants.AfterKeyAdded -= OnParticipantAdded;
            channelSession.Participants.BeforeKeyRemoved -= OnParticipantRemoved;
            channelSession.MessageLog.AfterItemAdded -= OnMessageLogRecieved;

            // Remove session.
            var user = _client.GetLoginSession(_accountId);
            user.DeleteChannelSession(channelSession.Channel);
        }

        // Trigger callback
        OnParticipantRemovedEvent?.Invoke(username, channel, participant);
    }

    private static void ValidateArgs(object[] objs)
    {
        foreach (var obj in objs)
        {
            if (obj == null)
                throw new ArgumentNullException(obj.GetType().ToString(), "Specify a non-null/non-empty argument.");
        }
    }
    #endregion

    private void VivoxLog(string msg)
    {
        Debug.Log("<color=green>VivoxVoice: </color>: " + msg);
    }

    private void VivoxLogError(string msg)
    {
        Debug.LogError("<color=green>VivoxVoice: </color>: " + msg);
    }
}