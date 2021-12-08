using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject myChatArea, othChatArea;
    public RectTransform ContentRect;
    public Scrollbar scrollBar;
    public InputField PrivateChat_InputField;
    public Button PrivateChat_Button_Send;
    public ScrollRect scroll_rect;
    ChatAreaManager LastArea;


    public void Chat(bool isSend, string text, string user, Texture picture)
    {
        if (text.Trim() == "") return; 

        bool isBottom = scrollBar.value <= 0.00001f;


        ChatAreaManager Area = Instantiate(isSend ? myChatArea : othChatArea).GetComponent<ChatAreaManager>();
        Area.transform.SetParent(ContentRect.transform, false);
        Area.BalloonRect.sizeDelta = new Vector2(130, Area.BalloonRect.sizeDelta.y);
        Area.TextRcet.GetComponent<Text>().text = text;
        Fit(Area.BalloonRect);


        float X = Area.TextRcet.sizeDelta.x + 20;
        float Y = Area.TextRcet.sizeDelta.y;
        if (Y > 25)
        {
            for (int i = 0; i < 200; i++)
            {
                Area.BalloonRect.sizeDelta = new Vector2(X - i * 2, Area.BalloonRect.sizeDelta.y);
                Fit(Area.BalloonRect);

                if (Y != Area.TextRcet.sizeDelta.y) { Area.BalloonRect.sizeDelta = new Vector2(X - (i * 2) + 2, Y); break; }
            }
        }
        else Area.BalloonRect.sizeDelta = new Vector2(X, Y);

        Area.User = user;
    }

    public void SendButtonOnClicked()
    {
        if (PrivateChat_InputField.text.Equals("")) return;
        else
        {
            Chat(true, PrivateChat_InputField.text, "  ", null);
            PretendingPrivateChat();
            scroll_rect.verticalNormalizedPosition = 0.0f;
            PrivateChat_InputField.text = "";
        }
    }

    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);

    async Task PretendingPrivateChat()
    {
        if (PrivateChat_InputField.text.Equals("오, 여기서 만나네"))
        {
            await Task.Delay(3500);
            Chat(false, "안녕!, 이거 대게 신기하다!", "B", null);
        }

        if (PrivateChat_InputField.text.Equals("우리 코끼리 동상에서 만나자!"))
        {
            await Task.Delay(1500);
            Chat(false, "오키오키, 우리 거기서 포토존 해보자!!!", "B", null);
        }
    }
}