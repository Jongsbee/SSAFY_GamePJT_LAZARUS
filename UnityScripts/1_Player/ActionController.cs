using JetBrains.Annotations;
using Opsive.UltimateCharacterController.Motion;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public static ActionController instance { get; private set; }

    public Sprite[] allSprites; // 드래그앤 드롭으로 할당
    public GameObject[] allItems; // 드래그앤 드롭으로 할당
    //public Dictionary<int, GameObject> allItemDictionary;
    //public Dictionary<int, Sprite> spriteDictionary; // 해당 아이템에 맞는 스프라이트
    //public Dictionary<int, int> itemCountDictionary; // 아이템이 몇개 있는지 <index, count>
    //public Dictionary<int, int> slotDictionary; // 아이템이 어느 위치에 있는지 <index, order>  
    public Dictionary<int, havingItemInfo> itemInfoDictionary;

    public static readonly int MAX_SLOT_COUNT = 20; // 최대 아이템 슬롯개수
    public static readonly int MAX_ITEM_COUNT = 99; // 최대 아이템 개수
    public static readonly int MAX_EQUIP_COUNT = 10; // 최대 장비 보유개수

    private bool pickupActivated = false;

    private RaycastHit hitinfo;

    [SerializeField]
    private LayerMask layerMask;

    SFXManager _sfxManager;

    public class havingItemInfo
    {

        public int itemIndex;
        public int slot;
        public int count;
        public Sprite sprite;
        public GameObject itemObject;

        public havingItemInfo(int itemIndex, Sprite sprite)
        {
            this.itemIndex = itemIndex;
            this.sprite = sprite;
            this.slot = -1; // 초기값을 -1로 해놓자
        }
    }

    // Update is called once per frame

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }

        itemInfoDictionary = new Dictionary<int, havingItemInfo>();
        //allItemDictionary = new Dictionary<int, GameObject>();
        //slotDictionary = new Dictionary<int, int>();
        //spriteDictionary = new Dictionary<int, Sprite>();
        //itemCountDictionary = new Dictionary<int, int>();

        foreach (var sprite in allSprites) // 모든 스프라이트를 하나씩 넣기
        {
            int itemIndex = int.Parse(sprite.name.Substring(0,3)); // 프리팹에서 앞의 3글자를 파싱하여 index 로 사용
            itemInfoDictionary.Add(itemIndex, new havingItemInfo(itemIndex, sprite)); // 
        }
        foreach (var item in allItems) // 아이템 오브젝트 넣기
        {
            int itemIndex = int.Parse(item.name.Substring(0, 3));
            itemInfoDictionary[itemIndex].itemObject = item; // 해당 오브젝트를 할당
            itemInfoDictionary[itemIndex].count = 0; // 초기값은 0으로 해준다.
        }

        _sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();

    }

    // 아이템을 획득한다.
    public void pickUpItem(Item item)
    {
        if (pickupActivated)
        {
            //Debug.Log(item.objectName + "획득했습니다");
            ItemInfoDisappear();
            if (UIManager.instance.inventory.AcquireItem(item.index, item.itemMaxCount, item.itemCount)) // 아이템을 획득하는데 성공하면
            {
                Destroy(item.gameObject); // 해당 아이템을 없애기
                _sfxManager.PickItem(item.index); // 해당 아이템 사운드 출력
            }  
        }
    }

    // 콜라이더에 닿았을 때, 아이템 정보가 나타난다
    public void ItemInfoAppear(Item item)
    {
        pickupActivated = true;
        UIManager.instance.actionTextBackground.gameObject.SetActive(true);
        if(item != null)
        {
            UIManager.instance.actionText.text = "Z키를 눌러 " + (ItemEnums.ItemNameKorIndex)item.index + " 획득 " + "<color=yellow>" + "(Z)" + "</color>";

            if (Input.GetKeyDown(KeyCode.Z)) // Z키를 눌렀을 때
            {
                // 아이템을 줏을 수 있따.
                pickUpItem(item);
            }
        }
    }

    // 아이템 info 사라지기
    public void ItemInfoDisappear()
    {
        pickupActivated = false;
        UIManager.instance.actionTextBackground.gameObject.SetActive(false);
    }
}
