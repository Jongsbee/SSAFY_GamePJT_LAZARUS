
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    private Transform uiCanvasTransform;

    public int[] allItemsHave;
    public GameObject[] craftLists, craftDescriptions;
    public readonly int MAX_CRAFT = 16; // 조합개수

    #region ShowText
    public GameObject actionTextBackground;
    public TMP_Text actionText;

    public GameObject bigNotification; // 큰 타이틀 알림창
    public TMP_Text bigActionText;

    public GameObject smNotification; // 작은 알림창
    public TMP_Text eventActionText;

    #endregion

    #region Inventory
    public GameObject inventoryUI;
    public Slot[] slots;
    public Image[] itemImage;
    public TMP_Text[] itemCountText;
    public GameObject[] itemCountImage;
    public RectTransform baseRect;
    //public InputNumber inputNumber;
    public SlotToolTip slotToolTip;
    //public DragSlot dragSlot;
    //public Image dragSlotImage;
    public TMP_Text inputNumberTextPreView;
    public TMP_InputField inputNumberInputField;
    public GameObject slotToolTipBase;
    public TMP_Text itemName;
    public TMP_Text itemDescription;
    public TMP_Text howToUse;
    public Inventory inventory;
    public GameObject inventoryBase;
    #endregion

    #region Craft
    public GameObject craftBase;
    public GameObject craftItemBigRockPanel;
    //public ShowBigRockRecipe showBigRockRecipe;
    public TMP_Text rockCountText;
    public Button itemBigRockRecipeBtn;
    public Button increaseCount;
    public Button decreaseCount;
    public Button confirmBtn;
    public TMP_InputField craftCount;

    public Dictionary<int, TMP_Text[]> craftIngredients; // 조합재료 텍스트



    #endregion

    #region HUD
    public RectTransform quickSlotBaseRect;
    public GameObject[] equipmentImgs; // 장비이미지들
    public TMP_Text arrowCntText, BulletCnt, ammoCnt;
    
    public StatusController statusController;
    public Image[] hud;

    #endregion

    #region Quest

    public GameObject questBoard;
    public GameObject[] questTitles; 
    public GameObject[][] questLists; // 퀘스트 리스트
    public TMP_Text journalCnt, journalText;

    #endregion

    #region Journal
    public GameObject[] journals;
    public GameObject journalPannel, journalLeftBtn, journalRightBtn;

    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        uiCanvasTransform = GameObject.Find("Manager").transform.Find("UICanvas").transform;

        craftLists = new GameObject[MAX_CRAFT]; // 조합창의 왼쪽 리스트
        craftDescriptions = new GameObject[MAX_CRAFT]; // 조합창의 오른쪽 리스트

        for(int i = 0; i < craftLists.Length; i++)  // for문 돌면서 할당
        {
            craftLists[i] = uiCanvasTransform.Find("3_Crafting/Crafting Panel/Left/LeftList/Content").GetChild(i).gameObject;
        }
        for (int i = 0; i < craftDescriptions.Length; i++) // for문 돌면서 할당
        {
            craftDescriptions[i] = uiCanvasTransform.Find("3_Crafting/Crafting Panel/Right/RightList").GetChild(i).gameObject;
            craftDescriptions[i].SetActive(false);
        }

        actionTextBackground = uiCanvasTransform.Find("5_ShowText/BackGround").gameObject;
        actionText = actionTextBackground.transform.Find("actionText").GetComponent<TMP_Text>(); // 콜라이더 이벤트시 나오는 Action Text


        bigNotification = uiCanvasTransform.Find("5_ShowText/TopNotification").gameObject;
        bigActionText = bigNotification.transform.Find("actionText").GetComponent<TMP_Text>(); // 일자 바뀔때 나오는 Action Text

        smNotification = uiCanvasTransform.Find("5_ShowText/Notification").gameObject;
        eventActionText = smNotification.transform.Find("actionText").GetComponent<TMP_Text>(); // 알림용으로 나오는 Action Text


        actionTextBackground.SetActive(false);
        bigNotification.SetActive(false);
        smNotification.SetActive(false);

    inventory = GameObject.Find("Manager").transform.Find("ItemManager").GetComponent<Inventory>();
        inventoryUI = uiCanvasTransform.transform.Find("1_Inventory").gameObject;
        inventoryBase = inventoryUI.transform.Find("Inventory_Base").gameObject;
        slots = inventoryBase.transform.Find("Grid Setting").GetComponentsInChildren<Slot>();
        for (int i = 0; i < ActionController.MAX_SLOT_COUNT; i++)
        {
            slots[i].InitState();
        }

        itemImage = new Image[ActionController.MAX_SLOT_COUNT];
        itemCountImage = new GameObject[ActionController.MAX_SLOT_COUNT];
        itemCountText = new TMP_Text[ActionController.MAX_SLOT_COUNT];
        for(int i = 0; i<ActionController.MAX_SLOT_COUNT; i++)
        {
            itemImage[i] = inventoryBase.transform.Find("Grid Setting").GetChild(i).GetChild(0).GetComponent<Image>();
            itemCountImage[i] = inventoryBase.transform.Find("Grid Setting").GetChild(i).GetChild(0).GetChild(0).gameObject;
            itemCountText[i] = inventoryBase.transform.Find("Grid Setting").GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        }

        

        baseRect = inventoryBase.GetComponent<RectTransform>();
        //inputNumber = inventory.transform.Find("InputNumber").GetComponent<InputNumber>();
        
        slotToolTip = inventoryUI.transform.Find("SlotToolTip").GetComponent<SlotToolTip>();
        quickSlotBaseRect = uiCanvasTransform.Find("0_HUD/QuickSlot").GetComponent<RectTransform>();
        equipmentImgs = new GameObject[5];
        for (int i = 0; i< equipmentImgs.Length; i++)
        {
            equipmentImgs[i] = quickSlotBaseRect.Find("EquipSlot").GetChild(i).gameObject;
            equipmentImgs[i].SetActive(false); // 우선 다 꺼두자

        }
        equipmentImgs[0].SetActive(true); // 주먹만 켜둔다
        arrowCntText = quickSlotBaseRect.Find("Arrows/Item_Image/TextImage/Count").GetComponent<TMP_Text>();
        BulletCnt = quickSlotBaseRect.Find("Bullets/Item_Image/TextImage/Count").GetComponent<TMP_Text>();
        ammoCnt = quickSlotBaseRect.Find("Bullets/Item_Image/AmmoText/Count").GetComponent<TMP_Text>();

        slotToolTipBase = slotToolTip.transform.Find("Base_Outer").gameObject;
        itemName = slotToolTipBase.transform.Find("Base_Inner/ItemName").GetComponent<TMP_Text>();
        itemDescription = slotToolTipBase.transform.Find("Base_Inner/ItemDescription").GetComponent<TMP_Text>();
        howToUse = slotToolTipBase.transform.Find("Base_Inner/HowToUse").GetComponent<TMP_Text>();
        

        craftBase = uiCanvasTransform.Find("3_Crafting/Crafting Panel").gameObject;

        statusController = GameObject.Find("Manager").transform.Find("EventManager").GetComponent<StatusController>();
        hud = new Image[3];
        hud[0] = uiCanvasTransform.Find("0_HUD/Status/Bar0/Hp_Bar").GetComponent<Image>();
        hud[1] = uiCanvasTransform.Find("0_HUD/Status/Bar1/Hunger_Bar").GetComponent<Image>();
        hud[2] = uiCanvasTransform.Find("0_HUD/Status/Bar2/Staminar_Bar").GetComponent<Image>();

        // 퀘스트

        questBoard = uiCanvasTransform.Find("0_HUD/Quest").gameObject;
        questTitles = new GameObject[3];
        for (int i = 0; i < questTitles.Length; i++)
        {
            questTitles[i] = questBoard.transform.GetChild(i).gameObject;
        }
        questLists = new GameObject[3][];

        questLists[0] = new GameObject[2];
        questLists[1] = new GameObject[3];
        questLists[2] = new GameObject[4];
        for (int i = 0;i < questLists.Length; i++)
        {
            for (int j = 0; j < questLists[i].Length; j++)
            {
                questLists[i][j] = questTitles[i].transform.GetChild(j).gameObject;
                questLists[i][j].SetActive(false); // 처음엔 다 꺼둔다
            }
            questTitles[i].SetActive(false); // 처음엔 타이틀도 다 꺼두자.
        }

    inventoryUI.SetActive(false); //게임에 들어가게 되면 기본적으로 UI는 OFF
        slotToolTipBase.SetActive(false);
        craftBase.SetActive(false);

        journals = new GameObject[10];
        for (int i = 0; i < journals.Length; i++)
        {
            journals[i] = uiCanvasTransform.Find("4_Journal/Journey").GetChild(i).gameObject;
            journals[i].SetActive(false);
        }
        journalPannel = uiCanvasTransform.Find("4_Journal").gameObject;
        journalLeftBtn = uiCanvasTransform.Find("4_Journal/Button/SelectButtonLeft").gameObject;
        journalRightBtn = uiCanvasTransform.Find("4_Journal/Button/SelectButtonRight").gameObject;

        journalPannel.SetActive(false);
        journalLeftBtn.SetActive(false);
        journalRightBtn.SetActive(false);
    }

    public void eventTextShow(string eventText)
    {
        StartCoroutine(eventTextCoroutine(eventText));
    }

    public IEnumerator eventTextCoroutine(string eventText)
    {

        eventActionText.text = eventText;
        smNotification.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        smNotification.SetActive(false);
    }

    public void titleTextShow(string eventText)
    {
        StartCoroutine(titleTextCoroutine(eventText));
    }

    public IEnumerator titleTextCoroutine(string eventText)
    {

        bigActionText.text = eventText;
        bigNotification.SetActive(true);

        yield return new WaitForSeconds(4.0f);

        bigNotification.SetActive(false);
    }

    public void changeEquip(int index)
    {
        for (int i=0; i<equipmentImgs.Length; i++)
        {
            equipmentImgs[i].SetActive(false);
        }
        equipmentImgs[index].SetActive(true);
    }

}
