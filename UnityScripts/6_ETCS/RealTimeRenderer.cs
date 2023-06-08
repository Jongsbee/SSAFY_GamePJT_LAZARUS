using InsaneSystems.Radar;
using UnityEngine;

public class RealTimeRenderer : MonoBehaviour
{
    public GameObject trees; // 전체 나무가 있는 object 
    public GameObject stones; // 전체 돌이 있는 object 
    public GameObject player; // player
    public float showDistance = 100f; // 어느정도 거리안의 오브젝트들을 활성화할지

    private void Awake()
    {
        trees = GameObject.Find("Map").transform.Find("Trees").gameObject;
        stones = GameObject.Find("Map").transform.Find("Stones").gameObject;
        player = GameObject.Find("Survival_Girl").gameObject;
        player.gameObject.GetComponent<RadarObject>().Initialize();
    }
    private void Start()
    {
        
    }
    void FixedUpdate() // fixedDeltaTime : 0.1f -> 0.1초마다 실행된다. 
    {
        for (int i = 0; i < trees.transform.childCount; i++) {

            //distance = Vector3.Distance(transform.position, trees.transform.GetChild(i).transform.position); // Tree와의 거리를 구하는 코드
            activateObj(Vector3.Distance(player.transform.position, trees.transform.GetChild(i).transform.position), trees.transform.GetChild(i).gameObject, 1);
        }
        for (int i = 0; i < stones.transform.childCount; i++)
        {
            activateObj(Vector3.Distance(player.transform.position, stones.transform.GetChild(i).transform.position), stones.transform.GetChild(i).gameObject, 2);
        }

    }

    public void activateObj(float distance, GameObject gameObject, int type)
    {
        // 거리가 일정 범위 내로 들어오면 오브젝트를 다시 활성화
        if (distance <= showDistance && !gameObject.GetComponent<Respawn>().isWaitingRespawn)
        {
            gameObject.SetActive(true);
        }
        
        else
        {
            gameObject.SetActive(false);
        }
    }
}
