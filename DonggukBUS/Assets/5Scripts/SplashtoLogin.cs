using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashtoLogin : MonoBehaviour
{
    [SerializeField] public GameObject logo;

    private void Update()
    {
        Debug.Log(logo.transform.position.x);
        if (logo.transform.position.x <= -410f)
        {
            SceneManager.LoadScene(1);
        }
        
    }
}
