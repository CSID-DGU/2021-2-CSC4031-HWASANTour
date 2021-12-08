using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.Networking;

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate () {
            OnClick(param);
        });
    }
}


public class AcademicManager : MonoBehaviour
{
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }


    [Serializable]
    public struct Notice
    {
        public string createDate;
        public string countView;
        public string title;
        public string noticeUrl;
    }

    Notice[] notice;


    /**
     *  학사공지 조회 결과 
     */
    void Start()
    {
        StartCoroutine(GetNotice());
    }

    void DrawUI()
    {
        GameObject buttonTemplate = transform.GetChild(0).gameObject;   //Button template
        GameObject g;

        int N = notice.Length;

        for (int i = 0; i < N; i++)
        {
            g = Instantiate(buttonTemplate, transform);
            g.transform.GetChild(0).GetComponent<Text>().text = notice[i].title;
            g.transform.GetChild(1).GetComponent<Text>().text = notice[i].countView;
            g.transform.GetChild(2).GetComponent<Text>().text = notice[i].createDate;

            g.GetComponent<Button>().AddEventListener(i, ItemClicked);
        }

        Destroy(buttonTemplate);
    }

    void ItemClicked(int itemIndex)
    {
        Debug.Log("name " + notice[itemIndex].title);
        Application.OpenURL(notice[itemIndex].noticeUrl);
    }

    IEnumerator GetNotice()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://54.64.195.1:9090/api/notices/all");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("www.error = " + www.error);
        }
        else
        {
            Debug.Log("www.downloadHandler.text = " + www.downloadHandler.text);
            notice = JsonHelper.FromJson<Notice>(www.downloadHandler.text);
            DrawUI();
        }
    }
}