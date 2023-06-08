using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class nextSceneTest : MonoBehaviour
{
    private GameObject player;
    private Vector3 newPosition;
    private bool loading = false;
    Button button;
    void Start()
    {
        player = CameraManager.instance.lobbyCharacter.gameObject;
        
        //인게임 씬에서의 위치
        newPosition = new Vector3(-65.5f, -5.63f, 320.1f);

        button = GetComponent<Button>();
        button.onClick.AddListener(MoveScene);
    }

    
   private void MoveScene()
    {
        ChangeSceneAndMoveObject(player, newPosition,"Main_Ingame");
        SceneManager.LoadScene("Main_Ingame");
    }

 
    //게임오브젝트(플레이어)를 옮기고, 씬 전환
    public void ChangeSceneAndMoveObject(GameObject go, Vector3 pos, string sceneName)
    {
        player = go;
        newPosition = pos;
        
        //OnSceneLoaded 이벤트를 추가함.
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    //비동기로 이루어지는 로딩 씬에서 로딩중에 대기화면을 설정함.
    IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            if (!loading)
            {
                loading = !loading;
                CameraManager.instance.loadingImage.gameObject.SetActive(true);
            }
            yield return null;
        }
    }

   
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (player != null)
        {
            player.transform.position = newPosition;
        }

        //OnSceneLoaded 이벤트 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
