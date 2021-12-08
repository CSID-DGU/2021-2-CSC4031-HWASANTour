using System.Collections;
using System.Collections.Generic;
using System.Data;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoginManager : MonoBehaviourPunCallbacks
{

    private readonly string gameVersion = "1";
    private readonly string PUN_METAVERSE_ROOM = "DGU";

    public static string LoginUserName;
    public static string LoginUserNickname;
    public static string LoginUserMajor;

    [SerializeField]
    public InputField inputField_ID;
    [SerializeField]
    public InputField inputField_PW;

    public Button Button_Login;



    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("[PUN] Online : Connected to Master Server");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {

        PhotonNetwork.ConnectUsingSettings();
        Debug.Log($"Offline : Connection Disalbed {cause.ToString()}");
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Create or Join Room");
            PhotonNetwork.JoinOrCreateRoom(PUN_METAVERSE_ROOM, new RoomOptions { MaxPlayers = 20 }, null);
        }
        else
        {
            Debug.Log("Offline : Connection Disalbed - Try reconnecting ...");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("There is no room, Initializing Metaverse Room");
        PhotonNetwork.CreateRoom(PUN_METAVERSE_ROOM, new RoomOptions { MaxPlayers = 20 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connected with Room.");
        PhotonNetwork.LoadLevel("3)MainScene");
    }

    // [HTTP] Login Request To Spring Web Server  
    public void OnClick()
    {
        Debug.Log("Login ID = " + inputField_ID.text.ToString());
        Debug.Log("Login Password = " + inputField_PW.text.ToString());
        string _username = inputField_ID.text.ToString();
        string _password = inputField_PW.text.ToString();

        var loginRequest = new LoginRequest(_username, _password)
        {
            username = _username,
            password = _password
        };

        var json = JsonConvert.SerializeObject(loginRequest);
        sendHttpRequest(json);
    }


    public void sendHttpRequest(string json)
    {
        StartCoroutine(httpRequestCoroutine(json));
    }

    private IEnumerator httpRequestCoroutine(string json)
    {

        using (UnityWebRequest request = UnityWebRequest.Post("http://54.64.195.1:9090/api/auth/login", json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();


            if ((request.result == UnityWebRequest.Result.ConnectionError) || (request.result == UnityWebRequest.Result.ProtocolError))
            {
                Debug.Log(request.error);

                JObject decoded = JObject.Parse(request.downloadHandler.text);

                string errorCode = decoded["code"].ToString();
                string message = decoded["message"].ToString();

                Debug.Log("errorCode = " + errorCode);
                Debug.Log("message = " + message);
            }
            else
            {
                // Login Response 200 (= Login Successfully)
                Debug.Log("Login Successfully");
                Debug.Log("request.responseCode = " + request.responseCode);

                JObject decoded = JObject.Parse(request.downloadHandler.text);

                string username = decoded["username"].ToString();
                string nickname = decoded["nickname"].ToString();
                string major = decoded["major"].ToString();


                Debug.Log("username = " + username);
                Debug.Log("nickname = " + nickname);
                Debug.Log("major = " + major);

                LoginUserName = username;
                LoginUserNickname = nickname;
                LoginUserMajor = major;

                // Connection to PUN
                Connect();

                // Move to Main Scene 
                //SceneManager.LoadScene(2);
            }
        }
    }
}