using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.Networking;
public class StagramManager : MonoBehaviour
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
        public class Wrapper<T>
        {
            public T[] Items;
        }
    }

    [Serializable]
    public struct Stagram
    {
        public Sprite Icon;
        public string imgDir;

    }

    Stagram[] stagram;
    [SerializeField] Sprite defaultIcon;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetGamesIcones());
    }

    void DrawUI()
    {
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        int N = stagram.Length;

        for (int i = 0; i < N; i++)
        {
            g = Instantiate(buttonTemplate, transform);
            g.transform.GetChild(0).GetComponent<Image>().sprite = stagram[i].Icon;


            g.GetComponent<Button>().AddEventListener(i, ItemClicked);
        }

        Destroy(buttonTemplate);
    }



    void ItemClicked(int itemIndex)
    {
        Debug.Log("instagram");
    }


    IEnumerator GetGamesIcones()
    {
        Debug.Log("GetGamesIcones()");

        UnityWebRequest www = UnityWebRequest.Get("http://54.64.195.1:9090/api/elephant/all");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("www.error = " + www.error);
        }
        else
        {
            Debug.Log("www.downloadHandler.text = " + www.downloadHandler.text);
            stagram = JsonHelper.FromJson<Stagram>(www.downloadHandler.text);
        }

        for (int i = 0; i < stagram.Length; i++)
        {
            WWW w = new WWW(stagram[i].imgDir);
            yield return w;

            if (w.error != null)
            {
                //error
                //show default image
                stagram[i].Icon = defaultIcon;
            }
            else
            {
                if (w.isDone)
                {
                    Texture2D tx = w.texture;
                    stagram[i].Icon = Sprite.Create(tx, new Rect(0f, 0f, tx.width, tx.height), Vector2.zero, 10f);
                }
            }
        }
        DrawUI();
    }
}