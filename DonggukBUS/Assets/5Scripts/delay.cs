using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class delay : MonoBehaviour
{
    public GameObject obj;
    public GameObject panel;

    public void onClick()
    {

    }
    public IEnumerator ActivationRoutine()
    {
        yield return new WaitForSeconds(14);

        obj.SetActive(false);

        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(3);

        //Game object will turn off
        panel.SetActive(true);
    }
}
