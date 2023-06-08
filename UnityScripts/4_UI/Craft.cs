using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Craft : MonoBehaviour
{
    public static Craft Instance { get; private set; }
    private bool isProcessing; // 멀티터치 방지
    public bool isCraftOpened;
    public int[][] craftTable; // 조합표
    SFXManager sfxManager;

    // Start is called before the first frame update

    private void Awake()
    {

        if(Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(Instance);
            Instance = this;
        }


        isProcessing = false;
        isCraftOpened = false;

        // 조합법 - [x,y ] 일경우 x를 y개 소모한다는 뜻
        craftTable = new int[400][];

        craftTable[1] = new int[] { 0, 3 }; // 가공목재
        craftTable[11] = new int[] { 10, 3 }; // 가공석재
        craftTable[61] = new int[] { 0, 3, 10, 3 }; // 화살 10개
        craftTable[60] = new int[] { 0, 5, 10, 5 }; // 모닥불
        craftTable[110] = new int[] { 0, 5, 10, 5 }; // 조잡한곡괭이
        craftTable[100] = new int[] { 0, 5, 10, 5 }; // 조잡한도끼
        craftTable[120] = new int[] { 0, 10, 10, 10 }; // 조잡한활
        craftTable[111] = new int[] { 1, 5, 11, 3, 110, 1 }; // 단단한곡괭이
        craftTable[101] = new int[] { 1, 5, 11, 3, 100, 1 }; // 단단한도끼
        craftTable[121] = new int[] { 1, 5, 11, 3, 120, 1 }; // 단단한활
        craftTable[200] = new int[] { 40, 1 }; // 구운 질긴고기
        craftTable[201] = new int[] { 41, 1 }; // 구운 맛좋은고기
        craftTable[202] = new int[] { 42, 1 }; // 구운 고단백고기
        craftTable[210] = new int[] { 40, 2, 30, 2 }; // 버섯탕수육
        craftTable[211] = new int[] { 41, 2, 30, 2, 31, 1 }; // 버섯 샤브샤브
        craftTable[212] = new int[] { 42, 2, 30, 1, 31, 1, 32, 1 }; // 송이버섯 고깃국
    }


    void Start()
    {
        sfxManager = GameObject.Find("Manager").transform.Find("SFXManager").GetComponent<SFXManager>();
    }



    // Update is called once per frame
    void Update()
    {
        TryOpenCraft();
    }

    private void TryOpenCraft()
    {
        if (Input.GetKeyDown(KeyCode.U) && !isProcessing) // U키를 눌렀을때 크래프트창 띄우기 or 띄워져있으면 닫기
        {
            
            if (!isCraftOpened) OpenCraft();
            else CloseCraft();

            StartCoroutine(PreventMultiTouch());
        }
    }

    public void OpenCraft() // 크래프트 창 열기
    {
        isCraftOpened = !isCraftOpened;
        UIManager.instance.craftBase.SetActive(true);

        // 열었을 때, 커서lock 을 해제하고, 커서를 돌려준다.
        ThirdPersonControllerForSurvive.uiClosed = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        

        sfxManager.PlaySFX("MakeItem");
    }

    public void CloseCraft() // 크래프트 창 닫기
    {
        isCraftOpened = !isCraftOpened;
        UIManager.instance.craftBase.SetActive(false);

        // UI 창이 다 꺼져야만 마우스 lock 을 하고 마우스를 숨긴다
        EventController.instance.returnPlayerControl();
        sfxManager.PlaySFX("MakeItem");
    }

    // 조합식 설명을 전체 false
    public static void initCraftDescription() 
    {
        for(int i = 0; i < UIManager.instance.MAX_CRAFT; i++) {
            UIManager.instance.craftDescriptions[i].SetActive(false);
        }
    }

    // 아이템을 조합한다.
    public bool craftItem(int craftItemIndex) // 아이템을 조합하는 함수
    {
        // 1. 조합하려는 재료가 충분하지 않다면
        if (!canCraft(craftItemIndex))
        {
            UIManager.instance.eventTextShow("재료가 충분하지 않습니다.");
            return false;
        }

        // 2. 아이템을 얻을 수 있다면 (인벤토리에 공간이 있거나, max count 를 넘지 않을 때 )
        if (!UIManager.instance.inventory.AcquireItem(craftItemIndex, ActionController.MAX_ITEM_COUNT, 1))
        
        {
            UIManager.instance.eventTextShow("인벤토리가 꽉찼습니다. 인벤토리를 비우고 다시 시도해주세요.");
            return false;
        }
        // 3. 위 두가지 모두 해당되지 않을때, for문을 돌면서 수행
        for (int i = 0; i < craftTable[craftItemIndex].Length / 2; i++) // for문을 돌면서 해당 조합 아이템들이 다 있는지 확인한다.
        {
            UIManager.instance.inventory.consumeItem(craftTable[craftItemIndex][i * 2], craftTable[craftItemIndex][i * 2 + 1]); // 각각의 아이템을 사용한다.
        }


        UserInfo.instance.itemCraftAPI(craftItemIndex); // 아이템 조합할 때 보내는 API
        return true;
    }

    // 조합할 수 있을지 확인하는 함수
    public bool canCraft(int itemIndex) 
    {
        for(int i = 0; i < craftTable[itemIndex].Length / 2; i++) // for문을 돌면서 해당 조합 아이템들이 다 있는지 확인한다.
        {
            if (ActionController.instance.itemInfoDictionary[craftTable[itemIndex][i * 2]].count < craftTable[itemIndex][i * 2 + 1]) // 필요한 재료가 조합되는 양보다 적으면 때
            {
                return false; // 재료가 부족하므로 false
            }
        }
        return true; // 재료가 부족하지 않다면 true
    }

    // 입력을 할 때, 멀티터치를 막는다.
    private IEnumerator PreventMultiTouch()
    {
        isProcessing = true;

        // 0.2초 동안 추가 입력을 막습니다.
        yield return new WaitForSeconds(0.2f);  

        isProcessing = false;
    }


}
