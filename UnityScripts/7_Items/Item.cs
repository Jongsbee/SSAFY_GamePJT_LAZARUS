using UnityEngine;


//Mouse RB can make this script.
[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : MonoBehaviour // 각각 아이템들마다 들어갈 스크립트
{
    public int index; // 아이템의 넘버
    public string objectName; // 오브젝트의 이름
    public string itemName; // 해당 아이템의 이름
    [TextArea]
    public string itemDescription;
    public ItemEnums.ItemType itemType;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public int itemMaxCount; //최대 소지 갯수, 초과 시에는 다른 칸을 차지하도록한다. ex ) 벽돌 10개 / 벽돌 10개 2칸을 차지한다.
    public int itemOrder; // 몇번째 슬롯에 있니?
    public int itemCount { get; set; }

    public string weaponType;

    public void Awake()
    {
        if (this.gameObject != null) // Object 에 있는 Item 이라면
        {
            objectName = this.gameObject.name; // 이 오브젝트의 이름
            index = int.Parse(objectName.Substring(0, 3)); // 앞에 3글자를 따서 인덱스화
            itemMaxCount = ActionController.MAX_ITEM_COUNT; // 최대 일단 99개로 해놓음
            itemName = ((ItemEnums.ItemNameKorIndex)index).ToString();
            itemCount = 1;
        }
    }

    void OnTriggerEnter(Collider other) // 콜라이더 이벤트가 일어났을 때
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }


}
