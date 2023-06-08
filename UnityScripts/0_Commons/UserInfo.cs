using System;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public static UserInfo instance;
    public string userName; // 사용자 이름
    public long userId; // 사용자 pk (Long)
    public int amount;  
    public string gameId; // 방의 넘버

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }



    // 1. 게임시작
    public void startGameAPI()
    {
        string url = MyAPIClient.BACKEND_URL + "/api/log/games/start";
        Guid uuid = Guid.NewGuid();
        gameId = uuid.ToString(); // 방 번호를 uuid 로 만듬
        long users = userId;

        string data = "{\"gameId\":\"" + gameId + "\", \"users\":\"" + users + "\"}";
        //Debug.Log(url);
        //Debug.Log(data);

        StartCoroutine(MyAPIClient.Instance.PostRequest<string>(url, data, logSuccess, logError));  

    }

    // 2. 아이템 소비 로그 제작

    public void itemUseAPI(int itemId)
    {
        string url = MyAPIClient.BACKEND_URL + "/api/log/logs/use";

        string data = "{\"userId\":\"" + userId
                                + "\", \"itemId\":\"" + itemId
                                + "\", \"gameId\":\"" + gameId + "\"}";

        //Debug.Log(url);
        //Debug.Log(data);

        StartCoroutine(MyAPIClient.Instance.PostRequest<string>(url, data, logSuccess, logError));

    }

    // 3. 음식 먹을때의 API
    public void itemEatAPI(int itemId)
    {
        string url = MyAPIClient.BACKEND_URL + "/api/log/logs/eat";

        string data = "{\"userId\":\"" + userId
                                + "\", \"itemId\":\"" + itemId
                                + "\", \"gameId\":\"" + gameId + "\"}";

        //Debug.Log(url);
        //Debug.Log(data);

        StartCoroutine(MyAPIClient.Instance.PostRequest<string>(url, data, logSuccess, logError));

    }

    // 4. 아이템 제작 시의 API
    public void itemCraftAPI(int itemId)
    {
        string url = MyAPIClient.BACKEND_URL + "/api/log/logs/craft";

        string data = "{\"userId\":\"" + userId
                                + "\", \"itemId\":\"" + itemId
                                + "\", \"gameId\":\"" + gameId + "\"}";

        //Debug.Log(url);
        //Debug.Log(data);

        StartCoroutine(MyAPIClient.Instance.PostRequest<string>(url, data, logSuccess, logError));

    }

    // 5. 퀘스트 완료 로그 저장

    public void questDoneAPI(int questId)
    {
        string url = MyAPIClient.BACKEND_URL + "/api/log/logs/quest";

        string data = "{\"userId\":\"" + userId
                                + "\", \"questId\":\"" + questId
                                + "\", \"gameId\":\"" + gameId + "\"}";

        //Debug.Log(url);
        //Debug.Log(data);

        StartCoroutine(MyAPIClient.Instance.PostRequest<string>(url, data, logSuccess, logError));

    }

    // 6. 몬스터 사냥 로그 저장

    public void huntAnimalAPI(GameObject animalObj)
    {
        string animalName = animalObj.name;
        //Debug.Log("동물이름 : " + animalName);
        int creatureId;
        string creatureType;

        // 현재 동물에 해당하는 index 가 없어서, 해당 object 의 name 으로 분류
        if (animalName.Contains("ZombiMoose")) { creatureId = 13; creatureType = "ELITE"; }
        else if (animalName.Contains("Boar")) { creatureId = 5; creatureType = "NORMAL"; }
        else if (animalName.Contains("ZombiStag")) { creatureId = 10; creatureType = "NORMAL"; }
        else if (animalName.Contains("ZombiWolf")) { creatureId = 11; creatureType = "NORMAL"; }
        else if (animalName.Contains("ZombiWolfBoss")) { creatureId = 11; creatureType = "ELITE"; }
        else if (animalName.Contains("MooseZombiBoss")) { creatureId = 13; creatureType = "ELITE"; }
        else if (animalName.Contains("WolfBoss")) { creatureId = 7; creatureType = "ELITE"; } // 고기가 3개
        else if (animalName.Contains("Wolf_Grey")) { creatureId = 6; creatureType = "NORMAL"; }
        else if (animalName.Contains("Wolf_Black")) { creatureId = 6; creatureType = "NORMAL"; }
        else if (animalName.Contains("StagBoss")) { creatureId = 4; creatureType = "ELITE"; }
        else if (animalName.Contains("Stag")) { creatureId = 3; creatureType = "NORMAL"; }
        else if (animalName.Contains("DeerBoss")) { creatureId = 2; creatureType = "ELITE"; }
        else if (animalName.Contains("Deer_DarkBrown")) { creatureId = 1; creatureType = "NORMAL"; }
        else if (animalName.Contains("Deer_Brown")) { creatureId = 1; creatureType = "NORMAL"; }
        else if (animalName.Contains("BearMinion")) { creatureId = 8; creatureType = "NORMAL"; }
        else if (animalName.Contains("BearBoss")) { creatureId = 9; creatureType = "ELITE"; }
        else if (animalName.Contains("Bear")) { creatureId = 8; creatureType = "NORMAL"; }
        else if (animalName.Contains("ZombiBearBoss")) { creatureId = 12; creatureType = "ELITE"; }
        else return;

        string url = MyAPIClient.BACKEND_URL + "/api/log/logs/hunt";

        string data = "{\"userId\":\"" + userId
                                + "\", \"creatureId\":\"" + creatureId
                                + "\", \"creatureType\":\"" + creatureType
                                + "\", \"gameId\":\"" + gameId + "\"}";

        StartCoroutine(MyAPIClient.Instance.PostRequest<string>(url, data, logSuccess, logError));

    }


    // 7. 게임 클리어 로그 저장  

    public void gameClearAPI(bool cleared)
    {
        string url = MyAPIClient.BACKEND_URL + "/api/log/logs/clear";
        
        string data = "{\"userId\":\"" + userId
                                + "\", \"gameId\":\"" + gameId
                                + "\", \"cleared\":\"" + cleared + "\"}";

        StartCoroutine(MyAPIClient.Instance.PostRequest<string>(url, data, logSuccess, logError));
    }


    // Return 값이 없으므로 success 와 error 를 API 공용으로 씀
    public void logSuccess(string result)
    {
        Debug.Log("API 성공");

    }
    public void logError(string result)
    {
        Debug.Log("API 실패");
    }
}


