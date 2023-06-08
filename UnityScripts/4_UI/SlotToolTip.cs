using UnityEngine;

public class SlotToolTip : MonoBehaviour
{
    // 툴팁 보여주기
    public void showToolTip(int itemIndex, Vector3 _pos)
    {
        //Debug.Log(itemIndex + " itemIndex");
        UIManager.instance.slotToolTipBase.SetActive(true);
        _pos += new Vector3(UIManager.instance.slotToolTipBase.GetComponent<RectTransform>().rect.width*0.5f, -UIManager.instance.slotToolTipBase.GetComponent<RectTransform>().rect.height*0.8f,0f);
        UIManager.instance.slotToolTipBase.transform.position = _pos;
        UIManager.instance.itemName.text = ((ItemEnums.ItemNameKorIndex)itemIndex).ToString();
        UIManager.instance.itemDescription.text = ItemEnums.ItemDescription[itemIndex].ToString();

        UIManager.instance.howToUse.text = "";
    }

    // 툴팁 숨기기
    public void HideToolTip()
    {
        UIManager.instance.slotToolTipBase.SetActive(false);
    }
}
