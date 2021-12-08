using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{

    public GameObject privateChatMenu, kokkiriMenu;
    public RectTransform privateChatMenuTr, kokkiriMenuTr;
    public GameObject PanelInfo;
    public RectTransform screen;
    public Camera uiCamera;

    private RaycastHit2D hit;
    private Vector2 MousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ����

        {
            MousePositionSetting();

            if (hit.collider != null)

            {
                if (hit.transform.gameObject.tag == "Info")
                {
                    PanelInfo.SetActive(true);
                }
            }

        }

        if (Input.GetMouseButtonDown(1)) //���콺 ������
        {
            MousePositionSetting();

            if (hit.collider != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(screen, Input.mousePosition, uiCamera, out MousePosition);

                if (hit.transform.gameObject.tag == "Player") //ĳ���� �±�
                {
                    privateChatMenuTr.localPosition = MousePosition;
                    privateChatMenu.SetActive(true);
                }
                else if (hit.transform.gameObject.tag == "Kokkiri") //�ڳ��� �±�
                {
                    kokkiriMenuTr.localPosition = MousePosition;
                    kokkiriMenu.SetActive(true);
                }
            }
        }
    }

    public void MousePositionSetting()
    {
        MousePosition = uiCamera.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(MousePosition);

        hit = Physics2D.Raycast(MousePosition, Vector2.zero);

        return;
    }
}