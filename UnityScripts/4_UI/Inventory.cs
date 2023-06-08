using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject fireWood; // 모닥불
    private GameObject player;  // Player 가져오기
    private bool isProcessing = false; // 멀티터치를 위한 bool변수
    public static Inventory Instance { get; private set; }
    SFXManager sfxManager;
    public bool isInventoryOpened; // 인벤토리 열렸는지 확인
    public int arrowRemain = 2000; // 화살 남은개수

    public List<int> slotHasItem;
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

        slotHasItem = new List<int>();
    }



    private void Start()
    {
        sfxManager = GameObject.Find("Manager").transform.Find("SFXManager").GetComponent<SFXManager>();
        player = GameObject.Find("Survival_Girl").gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }

    public void TryOpenInventory() // 인벤토리 토글
    {
        if (Input.GetKeyDown(KeyCode.I) && !isProcessing)
        {
            if (!isInventoryOpened)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }

            StartCoroutine(PreventMultiTouch());
        }
    }

    public void OpenInventory() // 인벤토리를 열자
    {
        isInventoryOpened = !isInventoryOpened;
        ThirdPersonControllerForSurvive.uiClosed = false;
        UIManager.instance.inventoryUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        sfxManager.PlaySFX("UIOpen");
    }

    public void CloseInventory() // 인벤토리 닫기
    {
        isInventoryOpened = !isInventoryOpened;
        UIManager.instance.inventoryUI.SetActive(false);

        // 모든 UI가 다 닫혔을 때, 넘겨주기.
        EventController.instance.returnPlayerControl();


        sfxManager.PlaySFX("UIClose");
    }

    // 아이템 획득 로직
    public bool AcquireItem(int itemIndex, int maxCount, int _count)
    {
        // 1. 인벤토리가 가득찼는지 확인
        if (checkInventoryIsFull()) 
        {
            UIManager.instance.eventTextShow("인벤토리가 가득찼습니다.");
            return false; 
        }

        // 2. 해당 아이템이 꽉 찼는지 체크
        if (checkItemIsFull(itemIndex, _count, maxCount))
        { 
            UIManager.instance.eventTextShow("해당 아이템을 더 가질 수 없습니다.");
            return false;
        }

 // 얻으려는 아이템을 가지고 있는지 확인하기

        if(ActionController.instance.itemInfoDictionary[itemIndex].slot >= 0) // 해당 아이템을 가지고 있다면
        {
                ActionController.instance.itemInfoDictionary[itemIndex].count += _count; // 아이템 카운트 + 1
                updateInventory(itemIndex, ActionController.instance.itemInfoDictionary[itemIndex].slot); // 인벤토리 UI 를 업데이트
                UIManager.instance.eventTextShow($"{(ItemEnums.ItemNameKorIndex)itemIndex} 획득하였습니다.");
                return true;
        }
        else // 해당 아이템을 가지고 있지 않다면
        {
            ActionController.instance.itemInfoDictionary[itemIndex].count += _count; // 아이템 카운트 + 1
            int slot = 0;
            for (int i = 0; i < 20; i++)
            {
                // for문을 돌면서 비어있는 슬롯을 찾기
                if (slotHasItem.Contains(i)) continue;
                else
                {
                    ActionController.instance.itemInfoDictionary[itemIndex].slot = i; // 새로운 슬롯을 더하기
                    slotHasItem.Add(i);
                    slot = i;
                    break;
                }
            }
            // 인벤토리가 가득 차지 않으면, 로직을 진행
            UIManager.instance.slots[slot].itemIndex = itemIndex;
            updateInventory(itemIndex, slot);
        }
            UIManager.instance.eventTextShow($"{(ItemEnums.ItemNameKorIndex)itemIndex} 획득하였습니다.");
            return true; 
    }


    // 인벤토리가 가득찼는지 확인
    public bool checkInventoryIsFull() 
    {

        return slotHasItem.Count >= ActionController.MAX_SLOT_COUNT ? true : false;
        //return UIManager.instance.slots.Length > _pos ? false : true;
    }

    // 해당 아이템이 최대 보유수인지 확인
    public bool checkItemIsFull(int index, int count, int maxCount) 
    {
        return ActionController.instance.itemInfoDictionary[index].count + count >= maxCount ? true : false;
    }


    // 인벤토리 UI 를 업데이트
    public void updateInventory(int index, int slot) 
    {
        UIManager.instance.itemImage[slot].sprite = ActionController.instance.itemInfoDictionary[index].sprite; // 스프라이트를 index 에 맞게 업데이트
        
        // 이미지의 컬러를 투명성 0으로 해놨다가 1로 바꾸어줌
        Color itemColor = UIManager.instance.itemImage[slot].color;
        itemColor.a = 1f;
        UIManager.instance.itemImage[slot].color = itemColor;
        UIManager.instance.itemCountImage[slot].SetActive(true); 
        UIManager.instance.itemCountText[slot].text = ActionController.instance.itemInfoDictionary[index].count.ToString(); // 아이템 개수 업데이트

    }

    // 아이템을 소모한다.
    public bool consumeItem(int index, int count) 
    {
       
        int slot = ActionController.instance.itemInfoDictionary[index].slot; // 해당 아이템의 슬롯번호를 찾기
        int itemCount = ActionController.instance.itemInfoDictionary[index].count;// 해당 아이템의 아이템개수
        if (count > itemCount) // 만약 부족하다면, 바로 return
        {
            return false;
        }
        
        ActionController.instance.itemInfoDictionary[index].count -= count; // 1개 차감하기
        updateInventory(index, slot);

        if (itemCount- count == 0) // 만약 아이템을 모두 소모하여 0개가 되었다면
        {
            ActionController.instance.itemInfoDictionary[index].slot = -1; // 해당 아이템의 슬롯을 제거하기
            slotHasItem.Remove(slot);
            UIManager.instance.itemImage[slot].sprite = null;

            // 컬러 초기화
            Color itemColor = UIManager.instance.itemImage[slot].color;
            itemColor.a = 0f;
            UIManager.instance.itemCountImage[slot].SetActive(false);
            UIManager.instance.itemImage[slot].color = itemColor;
            UIManager.instance.slots[slot].itemIndex = -1; // itemIndex 珥덇린??
        }

        return true;
    }

    // 아이템 사용하였을 때 효과
    public void useItemInSlots(int itemIndex)
    {
        sfxManager.ConsumeSFX(itemIndex);
        switch(itemIndex)
        {
            case 30: StatusController.instance.IncreaseHungry(10);
                break;
            case 31:
                StatusController.instance.IncreaseHungry(10);
                StatusController.instance.DecreaseHP(3);
                break;
            case 32: StatusController.instance.IncreaseHungry(5);
                break;
            case 40: StatusController.instance.IncreaseHungry(15);
                break;
            case 41:StatusController.instance.IncreaseHungry(20);
                break;
            case 42:StatusController.instance.IncreaseHungry(30);
                break;
            case 200: StatusController.instance.IncreaseHungry(15);
                StatusController.instance.IncreaseHP(5);
                break;
            case 201:
                StatusController.instance.IncreaseHungry(20);
                StatusController.instance.IncreaseHP(8);
                break;
            case 202:
                StatusController.instance.IncreaseHungry(30);
                StatusController.instance.IncreaseHP(10);
                break;
            case 210:
                StatusController.instance.IncreaseHungry(30);
                StatusController.instance.IncreaseHP(15);
                break;
            case 211:
                StatusController.instance.IncreaseHungry(50);
                StatusController.instance.IncreaseHP(30);
                break;
            case 212:
                StatusController.instance.IncreaseHungry(80);
                StatusController.instance.IncreaseHP(50);
                break;

                // 장비류 - 장착하면 데미지 변환
            case 100: 
                ItemEnums.DamageTable[2] = 40;
                break;
            case 101:
                ItemEnums.DamageTable[2] = 80;
                break;
            case 110: 
                ItemEnums.DamageTable[1] = 40;
                break;
            case 111:
                ItemEnums.DamageTable[1] = 80;
                break;
            case 120: 
                ItemEnums.DamageTable[3] = 40;
                break;
            case 121:
                ItemEnums.DamageTable[3] = 80;
                break;
            

            // 소모품 사용
            case 60: // 캠프파이어
                fireWood = Resources.Load<GameObject>("Campfire Item_025");
                Instantiate(fireWood, (player.transform.position + player.transform.forward + new Vector3(0, 2, 0)), Quaternion.identity); // 캠프파이어 소환
                break;
            
            case 61: // 화살
                arrowRemain += 20;
                UIManager.instance.arrowCntText.text = arrowRemain.ToString();

                break;
            case 62: // 총
               
                AssaultRifle.ammoRemain += 20; 
                UIManager.instance.BulletCnt.text = AssaultRifle.ammoRemain.ToString();
                break;

            default:
                Debug.Log("잘못된 아이템 번호입니다.");
                break;
        }

        switch (ItemEnums.ItemTypeDict[itemIndex])
        {
            case 0: // 장비일경우

                break;
            case 1: // 소모품일경우
                consumeItem(itemIndex, 1);
                UserInfo.instance.itemUseAPI(itemIndex);

                break;
            case 3: // 음식일경우
                consumeItem(itemIndex, 1);
                UserInfo.instance.itemEatAPI(itemIndex);
                break;

        }
    }

    // 멀티터치를 막는 스크립트
    private IEnumerator PreventMultiTouch()
    {
        isProcessing = true;

        // 0.3 초동안 못누르게하기
        yield return new WaitForSeconds(0.3f);

        isProcessing = false;
    }

}
