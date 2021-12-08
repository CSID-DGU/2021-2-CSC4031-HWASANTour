using UnityEngine;

public class InputButton : MonoBehaviour
{
    public static float VerticalInput;

    /* ���콺 �Ǵ� Ű����� ��ǲ �ް� ���� */
    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("DownArrow");
            VerticalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("UpArrow");
            VerticalInput = 1f;
        }
        else
        {
            VerticalInput = 0f;
        }
    }
}