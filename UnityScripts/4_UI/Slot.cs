using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
    ,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int slotIndex;
    public int itemIndex;
    
    // 초기상태 설정
    public void InitState()
    {
        slotIndex = int.Parse(gameObject.name.Substring(gameObject.name.Length - 2, 2));
        itemIndex = -1;
    }


    // 오른쪽 클릭으로 아이템 소모
    public void OnPointerClick(PointerEventData eventData) 
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log(slotIndex + " slotIndex");
            Debug.Log(itemIndex + " itemIndex");

            if (itemIndex != -1)
            {
                //consume
                Inventory.Instance.useItemInSlots(itemIndex);

                Debug.Log((ItemEnums.ItemNameKorIndex)itemIndex + " 을 사용했습니다.");
                //SetSlotCount(item.itemOrder, -1);
            }
        }
    }

    // 마우스 호버로 정보 보기
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemIndex != -1)
        {
            UIManager.instance.slotToolTip.showToolTip(itemIndex, transform.position);
        }
    }

    // 마우스 호버 끝
    public void OnPointerExit(PointerEventData eventData) 
    {
        UIManager.instance.slotToolTip.HideToolTip();
    }
}
