using InsaneSystems.Radar;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{

    public GameObject[] EventObjectDrops; // 활성화, 비활성화될 이벤트 오브젝트들
    public bool[] QuestAchieved; // 퀘스트가 완료되었는지 확인하는 배열
    public readonly int MAX_JOURNEY_CNT = 10; // 최대 일지의 개수
    private bool isProcessing = false; // 다중 클릭을 막기위해 코루틴 사용
    private bool hasNextJounrey, hasPreviousJourney, isJournalOpened, isQuestOpened; // 각각 일지에서 다음페이지 존재여부, 전페이지 존재여부, 일지와 퀘스트창 오픈여부
    public int currentPageIndex; // 현재 내 페이지
    private int previousIndex, presentIndex, nextIndex; // 일지에서 이전 index, 지금 index, 다음 index

    SFXManager sfxManager;
    public static List<int> JournalKeyList; // 존재하는 키들을 담은 리스트
    
    
    public static EventController instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(instance);
            instance = this;
        }
        currentPageIndex = -1; // 초기 값은 -1
        JournalKeyList = new List<int>();
        isJournalOpened = false;
        QuestAchieved = new bool[31]; // 0~9 : 작동여부, 10 : 다모은 여부, 11~15 : 봉인석 여부, 16 :  묘지 해제, 17 : 정해석(소), 18 : 정해석(대), 19 : 시약, 20 : 샘 불태우기
                                       // 21 : 연구자료 (탈출가능여부), 22 : 통신기 (원래갖고있음), 23 : 3일 지났는지 판단, 24 : 안개꼈는지 판단,  25 : 장작붙이기, 30 : 탈출가능여부
    }


    private void Update()
    {
        TryOpenJournal();
        TryOpenQuest();
    }

    private void Start()
    {
        sfxManager = GameObject.Find("Manager").transform.Find("SFXManager").GetComponent<SFXManager>();
        initJournal(); // 초기상태 활성
        UserInfo.instance.startGameAPI(); // 게임 시작
        Time.fixedDeltaTime = 0.1f; // fixedDeltaTime 설정 - 0.1초마다 fixedUpdate 실행하기
    }

    public void TryOpenJournal() // 인벤토리 열고닫기
    {
        if (Input.GetKeyDown(KeyCode.J) && !isProcessing) // J키로 열고닫게 
        {
            if (!isJournalOpened)
            {
                OpenJourney();
            }
            else
            {
                CloseJourney();
            }

            StartCoroutine(PreventMultiTouch());
        }
    }

    public void TryOpenQuest() // 퀘스트창 열고닫기
    {
        if (Input.GetKeyDown(KeyCode.Q)) // J키로 열고닫게 
        {
            if (!isQuestOpened)
            {
                OpenQuest();
            }
            else
            {
                CloseQuest();
            }
        }
    }


    public void OpenJourney() // 일지를 연다
    {
        isJournalOpened = !isJournalOpened;
        Debug.Log($"현재페이지 : {currentPageIndex}, 이전페이지 : {previousIndex}, 다음페이지 : {nextIndex}");
        UIManager.instance.journalPannel.SetActive(true);
        ThirdPersonControllerForSurvive.uiClosed = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //sfxManager.PlaySFX("UIOpen");
    }

    public void CloseJourney() // 일지를 닫는다
    {
        isJournalOpened = !isJournalOpened;
        UIManager.instance.journalPannel.SetActive(false);
        returnPlayerControl();
        //sfxManager.PlaySFX("UIClose");
    }

    public void OpenQuest() // 퀘스트창 열고닫기
    {
        isQuestOpened = !isQuestOpened;
        UIManager.instance.questBoard.SetActive(true);
    }

    public void CloseQuest() // 퀘스트창을 닫는다.
    {
        isQuestOpened = !isQuestOpened;
        UIManager.instance.questBoard.SetActive(false);
    }


    // 퀘스트 및 이벤트를 진행
    public void doEvent(EventItem eventItem)
    {
        //Debug.Log(eventItem.eventNum + "번 이벤트 진행");
        eventInfoDisappear();
        if(eventItem.eventNum <= 9 ) // Case 1. Jornal 일때
        {
            eventItem.gameObject.GetComponent<BoxCollider>().enabled = false; // 콜라이더를 먼저 비활성화
            eventItem.gameObject.GetComponent<RadarObject>().enabled = false; // 레이더도 비활성화
            Destroy(eventItem.gameObject); // 해당 아이템을 파괴
            JournalKeyList.Add(eventItem.eventNum); // Jornal List 에 하나씩 true로 바꿔서 추가한다.
            JournalKeyList.Sort(); // 정렬

            // 획득한 일지에 따라서, 퀘스트 내용을 on
            switch (eventItem.eventNum)
            {

                case 0:
                    UIManager.instance.questTitles[0].SetActive(true);
                    UIManager.instance.questLists[0][0].SetActive(true);
                    break;
                case 2:
                    UIManager.instance.questLists[0][1].SetActive(true);
                    UIManager.instance.questTitles[1].SetActive(true);
                    UIManager.instance.questLists[1][0].SetActive(true);
                    UIManager.instance.questLists[1][1].SetActive(true);

                    break;
                case 7:
                    UIManager.instance.questLists[2][1].SetActive(true);
                    break;
                case 8:
                    UIManager.instance.questLists[2][2].SetActive(true);
                    break;
                case 9:
                    UIManager.instance.questLists[2][3].SetActive(true);
                    break;
            }

            if (currentPageIndex == -1) // 처음 일지를 획득한 상태라면
            {
                currentPageIndex = eventItem.eventNum; // 현재 페이지 인덱스를 획득한 일지의 넘버로 한다.
                UIManager.instance.journals[currentPageIndex].SetActive(true); // 가장먼저 먹은 첫장을 켠다.
                initJournal(); // 설정상태를 다시 초기화
                checkPages(); // 
                Debug.Log($"현재페이지 : {currentPageIndex}, 이전페이지 : {previousIndex}, 다음페이지 : {nextIndex}");
            }
            // 처음 먹은거면 해당 인덱스로 인덱스를 변경한다.
            checkPages(); // Journey 갱신

            QuestAchieved[eventItem.eventNum] = true; // 해당 인덱스의 모으기 완료
            UIManager.instance.eventTextShow("일지 획득. J키를 눌러 확인해보세요.");
            UserInfo.instance.questDoneAPI(eventItem.eventNum); // 일지모으기    
            if (JournalKeyList.Count == 9)
            {
                QuestAchieved[10] = true; // 일지를 다모으면 ok
                UIManager.instance.eventTextShow("일지를 전부 모았습니다.");
                UserInfo.instance.questDoneAPI(10);
            }
        }
        else if(eventItem.eventNum == 11 ) {

            if (!QuestAchieved[17])
            {
                UIManager.instance.eventTextShow("해제할 수 없습니다. 룬석이 부족합니다.");
                // 정해석을 가지고 있지 않다면 경고
                return; 
            }

            // 정해석을 가지고 있다면 

            int eventItemIndex = int.Parse(eventItem.gameObject.name.Substring(eventItem.gameObject.name.Length - 2, 2)); // 뒤의 두글자를 인덱스로 받아오기
            eventItem.gameObject.GetComponent<SphereCollider>().enabled = false; // 해당 오브젝트 활성화 후 콜라이더 끄기

            QuestAchieved[eventItemIndex] = true; // 해당 오브젝트 활성화 완료
            UserInfo.instance.questDoneAPI(eventItemIndex);
            checkSealOpened(); // 엑조디아 5파트 다모았는지 확인

            UIManager.instance.eventTextShow("봉인을 해제하였습니다.");
        }
        else if (eventItem.eventNum == 16) // 천사석상 해제 - 타워 해금 [구현완 - 룬스톤 사라지는 소리넣으면 좋음]
        {
            EventObjectDrops[0].gameObject.SetActive(false); // 타워에 있는 룬스톤이 사라진다.

            eventItem.gameObject.GetComponent<BoxCollider>().enabled = false; // 이벤트 콜라이더를 끄기

            QuestAchieved[16] = true;

            UIManager.instance.eventTextShow("작동완료. 어딘가의 입구가 열렸습니다."); ;
            UserInfo.instance.questDoneAPI(16);
        }
        else if (eventItem.eventNum == 17)  // 퀘스트 인벤토리에 획득하는 코드 필요
        {
            // 정해석 획득
            eventItem.gameObject.GetComponent<BoxCollider>().enabled = false; // 이벤트 콜라이더를 끄기
            Destroy(eventItem.gameObject);
            UIManager.instance.questTitles[2].SetActive(true);
            QuestAchieved[17] = true;

            UIManager.instance.eventTextShow("룬석을 획득하였습니다. 봉인을 해제할 수 있습니다.");
            UserInfo.instance.questDoneAPI(17);
        }
        else if (eventItem.eventNum == 18) // 정해석 획득 - 퀘스트아이템 등록 필요
        {
            eventItem.gameObject.GetComponent<SphereCollider>().enabled = false; // 이벤트 콜라이더를 끄기
            Destroy(eventItem.gameObject);

            QuestAchieved[18] = true;

            UIManager.instance.eventTextShow("정해석을 획득하였습니다.");
            UserInfo.instance.questDoneAPI(18);
        }
        else if(eventItem.eventNum == 19) // 시약샘플 얻기- 퀘스트아이템 등록 필요
        {
            eventItem.gameObject.GetComponent<SphereCollider>().enabled = false; // 이벤트 콜라이더를 끄기
            Destroy(eventItem.gameObject);

            QuestAchieved[19] = true;

            UIManager.instance.eventTextShow("시약샘플을 획득하였습니다.");
            UserInfo.instance.questDoneAPI(19);

        }
        else if(eventItem.eventNum == 20) // 샘 정화하기 
        {
            if (!QuestAchieved[18])
            {
                UIManager.instance.eventTextShow("열쇠가 없으므로 샘을 정화할 수 없습니다. 일지에서 답을 찾아주세요.");
                return;

            }

            eventItem.gameObject.GetComponent<BoxCollider>().enabled = false; // 이벤트 콜라이더를 끄기

            EventObjectDrops[2].SetActive(false);

            UIManager.instance.eventTextShow("샘을 정화하였습니다. 오랜시간에 거쳐 샘은다시 찰것입니다.");
            UserInfo.instance.questDoneAPI(20);
        }
        else if (eventItem.eventNum == 21) // 연구자료 - 17번과 같이 퀘스트 아이템에 들어가야함
        {
            UIManager.instance.questLists[1][2].SetActive(true);
            eventItem.gameObject.GetComponent<BoxCollider>().enabled = false; // 콜라이더를 먼저 비활성화
            Destroy(eventItem.gameObject); // 해당 아이템을 파괴
            QuestAchieved[21] = true;
            UIManager.instance.questLists[1][2].SetActive(true) ;

            UIManager.instance.eventTextShow("핵심 연구자료를 획득하였습니다. 이제 탈출할 수 있습니다.");
            UserInfo.instance.questDoneAPI(21);
        }
        else if (eventItem.eventNum == 25) // 캠프파이어
        {
            
            CampFire campFire = eventItem.gameObject.GetComponent<CampFire>();
            campFire.StartFire();
            UIManager.instance.eventTextShow("불을 피웁니다. 밤이 무섭지 않습니다.");
        }
        else if (eventItem.eventNum == 30) // 헬리콥터 부르기
        {
           

            if (QuestAchieved[19] && QuestAchieved[20] && QuestAchieved[21] && QuestAchieved[10]) // 일지 다모으기 & 샘정화하기 & 연구자료 모으기 & 시약샘플 얻기
            {   
                // 진엔딩
                UIManager.instance.eventTextShow("진엔딩에 도달하였습니다.");
                UserInfo.instance.gameClearAPI(true);
                UserInfo.instance.questDoneAPI(30);
                return;
            }
            if (QuestAchieved[21])
            {
                UIManager.instance.eventTextShow("자료를 가지고 탈출에 성공하였지만, 어딘가 찜찜한 기분입니다.");
                UserInfo.instance.gameClearAPI(true);
                UserInfo.instance.questDoneAPI(29);
                return;
            }
        }
        sfxManager.Event(eventItem.eventNum);
        eventInfoDisappear();
    }

    // 룬석의 봉인해제를 체크
    public void checkSealOpened()
    {
        int count = 0;

        for (int i = 11; i <= 15; i++)
        {
            if (QuestAchieved[i]) count++;
        }
        if(count == 5)
        {
            EventObjectDrops[1].gameObject.SetActive(false);
            UserInfo.instance.questDoneAPI(16);
            UIManager.instance.eventTextShow("봉인석을 모두 해제하였습니다. 어딘가의 문이 열립니다.");
        }

    }

    // 이벤트 트리거 ON
    public void EventInfoAppear(GameObject gameObject) 
    {
        EventItem eventItem = gameObject.GetComponent<EventItem>();

        UIManager.instance.actionTextBackground.gameObject.SetActive(true);
        UIManager.instance.actionText.text = "E키를 눌러 " + (EventEnums.EventEnum) eventItem.eventNum + "<color=blue>" + "(E)" + "</color>";

        if (Input.GetKeyDown(KeyCode.E) && !isProcessing) // E키를 눌렀을 때
        {   
            //CheckItem();
            doEvent(eventItem); // 이벤트를 실행할 수 있도록 함

            StartCoroutine(PreventMultiTouch()); // 코루틴으로 멀티클릭 방지

        }
            
    }


    // 이벤트 콜라이더 사라지게하는 메서드
    public void eventInfoDisappear() 
    {
        UIManager.instance.actionTextBackground.gameObject.SetActive(false);
    }


    private IEnumerator PreventMultiTouch()
    {
        isProcessing = true;

        // 0.5초 동안 추가 입력을 막습니다.
        yield return new WaitForSeconds(0.5f);

        isProcessing = false;
    }

    public void changePage(GameObject gameObject) // 버튼 클릭 이벤트로, 일지의 페이지를 넘긴다.
    {
        // 왼쪽 오른쪽 눌렀을 때
        if (gameObject.name.Contains("Left")) // 왼쪽을 눌렀다면
        {
            UIManager.instance.journals[currentPageIndex].SetActive(false); // 현재창을 닫는다.
            UIManager.instance.journals[previousIndex].SetActive(true); // 이전 일지를 연다
            currentPageIndex = previousIndex;
        }
        else
        {
            UIManager.instance.journals[currentPageIndex].SetActive(false); // 현재창을 닫는다.
            UIManager.instance.journals[nextIndex].SetActive(true); // 다음 일지를 연다
            currentPageIndex = nextIndex;
        }

        // 다시 초기화

        initJournal();
        checkPages();

        Debug.Log($"현재페이지 : {currentPageIndex}, 이전페이지 : {previousIndex}, 다음페이지 : {nextIndex}");
    }


    // 현재 인덱스에서 다음, 이전 페이지가 존재하는지 확인하는 메서드
    public void checkPages()
    {
        if (currentPageIndex == -1 && JournalKeyList.Count == 0) // 획득한 페이지가 없으면 아무일도 일어나지 않는다.
        {
            return;
        }
        
        else // 그게 아니라면 nowIndex는 -1이 아닐거임
        {
            foreach (int key in JournalKeyList) // 보유하고있는 dictionary 의 key들
            {
                if (key < currentPageIndex)
                {
                    hasPreviousJourney = true;
                    previousIndex = key;
                }// 현재 인덱스보다 작은 key값이 있으면 - 이전페이지 존재
                if (key > currentPageIndex)
                {
                    hasNextJounrey = true;
                    if (presentIndex == currentPageIndex || presentIndex == -1) // 현재페이지 바로 다음페이지가 다음페이지가 된다.
                    {
                        nextIndex = key;
                    }
                    presentIndex = key;
                }// 현재 인덱스보다 큰 key값이 있으면 - 다음 페이지 존재

            }
            if (hasPreviousJourney) UIManager.instance.journalLeftBtn.SetActive(true); // 이전이 있다면 left 활성화
            if(hasNextJounrey) UIManager.instance.journalRightBtn.SetActive(true); // 이전이 있다면 right 활성화

        }

    }

    public void initJournal() // Journal 초기화
    {

        previousIndex = -1;
        presentIndex = -1;
        nextIndex = -1;


        hasNextJounrey = false;
        hasPreviousJourney = false;

        UIManager.instance.journalLeftBtn.SetActive(false);
        UIManager.instance.journalRightBtn.SetActive(false);
    }

    public void returnPlayerControl() // 커서되돌려주는 로직
    {
        if(isJournalOpened || Inventory.Instance.isInventoryOpened || Craft.Instance.isCraftOpened) // 셋중 하나라도 열려있다면 패스
        {
            return;
        }else // 그렇지 않다면 닫기
        {
            ThirdPersonControllerForSurvive.uiClosed = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


    }
}
