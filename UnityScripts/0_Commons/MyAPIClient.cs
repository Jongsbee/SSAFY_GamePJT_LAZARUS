using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


public class MyAPIClient :MonoBehaviour
{
    public static MyAPIClient Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //public static readonly string BACKEND_URL = "http://localhost:8200/";
    public static readonly string BACKEND_URL = "https://k8a106.p.ssafy.io/";
    //public static readonly string BACKEND_URL = "http://localhost:8080/";

    // 1. Get 요청 - 해당 URL로 GET 요청을 보낸다.
    public IEnumerator GetRequest<T>(string url, System.Action<T> onSuccess, System.Action<T> onError)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Get 요청 실패!");
                onError?.Invoke(default(T));
            }
            else
            {
                Debug.Log("Get 요청 성공!");
                string jsonString = request.downloadHandler.text;
                T response;
                try // Json 을 Deserialize 한다
                {
                    response = JsonConvert.DeserializeObject<T>(jsonString);
                }
                catch (JsonReaderException) // json 값이 아닌 일반 스트링일때
                {
                    response = (T)Convert.ChangeType(jsonString, typeof(T));
                }

                onSuccess?.Invoke(response); // 성공했을 시 해당 값을 return
            }
        }
    }
    
    // 해당 URL과 data (String Json 값) 로 POST 요청을 보낸다.

    // 2. Post 요청   
    public IEnumerator PostRequest<T>(string url, string data, System.Action<T> onSuccess, System.Action<T> onError)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, ""))
        {
            // Json parse - Data를 Json 형식으로 바꾸어서 보낸다.
            byte[] jsonToSend = Encoding.UTF8.GetBytes(data);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // 응답이 올때까지 기다린 후 return
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) // 성공하지 못했다면
            {
                Debug.Log("POST 전송 실패!!");
                onError?.Invoke(default(T));
            }
            else
            {
                Debug.Log("POST 전송 성공!");

                string jsonString = request.downloadHandler.text;
                T response;
                try
                {
                    response = JsonConvert.DeserializeObject<T>(jsonString);
                }
                catch (JsonReaderException)
                {
                    response = (T)Convert.ChangeType(jsonString, typeof(T));
                }

                onSuccess?.Invoke(response);
            }
        }
    }
}
