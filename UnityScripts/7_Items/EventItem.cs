using UnityEngine;

public class EventItem : MonoBehaviour // 이벤트 아이템들 마다 들어갈 스크립트
{
    public int eventNum; // 번호 - 직접할당 후 프리팹화
    public string objectName; // 오브젝트의 이름
    public string eventName; // 해당 아이템의 이름

    private void Awake()
    {
        objectName = gameObject.name;
        eventName = ((EventEnums.EventEnum)eventNum).ToString();
    }

    void OnTriggerEnter(Collider other) // 콜라이더 이벤트가 일어났을 때
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
