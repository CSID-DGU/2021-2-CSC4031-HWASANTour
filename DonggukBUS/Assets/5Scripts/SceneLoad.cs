using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] public GameObject Panel;
    [SerializeField] public Text loadtext;
    [SerializeField] public GameObject logo;
    [SerializeField] private Image backgroundblack;
    [SerializeField] public AudioSource Audio;

    private void Start()
    {
        StartCoroutine(LoadScene()); //LoadScene 실행
    }

    public void Animate()
    {
        Panel.SetActive(true);
        loadtext.text = "▶ Clik  to  start  ◀";
        if (Input.GetKeyDown(KeyCode.Mouse0)) // 마우스 좌클릭
        {
            var sequence = DOTween.Sequence()
            .Append(logo.transform.DOShakePosition(3.61f,2,10).SetEase(Ease.OutQuad))
            .Append(logo.transform.DOMoveX(-700, 2).SetEase(Ease.InBack).SetDelay(0.27f))
            .Join(backgroundblack.DOFillAmount(1f, 1f).SetEase(Ease.OutCubic).SetDelay(1.5f))
            .AppendInterval(1.5f);
        }

    }

    IEnumerator LoadScene()
    {
        yield return null;
        while (true)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Debug.Log(logo.transform.position.x);
                if (logo.transform.position.x <= -410f)
                {
                    SceneManager.LoadScene(1);
                }
                Debug.Log(logo.transform.position.x);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                yield return null;
                AsyncOperation operation = SceneManager.LoadSceneAsync(3); //MainScene을 비동기Load
                operation.allowSceneActivation = false; //로딩이 끝나도 대기(progress=0.9f)

                while (!operation.isDone)
                {
                    yield return null;

                    if (operation.progress < 0.9f)
                    {
                        loadtext.text = "Loading... " + (operation.progress * 100) + "%";
                        Debug.Log(operation.progress);
                    }
                    else
                    {
                        loadtext.text = "▶ Clik  to  start  ◀";

                        if (Input.GetKeyDown(KeyCode.Mouse0)) // 마우스 좌클릭
                        {
                            Audio.Play();
                            Animate();
                        }
                        if (logo.transform.position.x <= -700f)
                        {
                            operation.allowSceneActivation = true;
                            //PhotonNetwork.LoadLevel("3)MainScene");
                        }
                    }
                }
            }
        }
    }
}
