using UnityEngine;
public class CampFire : MonoBehaviour
{

    [SerializeField] private GameObject onFire;
    [SerializeField] private GameObject offFire;
    private bool isFire;
    private bool playerHere;

    [SerializeField] private float fireLifeTime = 90f;
    [SerializeField] private float currentFireLifeTime;

    private void Awake()
    {
        onFire = transform.Find("Campfire").gameObject;
        offFire = transform.Find("TorchSmoke").gameObject;
    }

    private void Start()
    {
        isFire = false;
        onFire.SetActive(false);
        //StartFire();
        playerHere = false;

    }

    // Update is called once per frame
    public void Update()
    {
        if (isFire) 
        {
            FireTime();
        }
    }


    // 불이 켜져있을 때의 상태
    private void FireTime()
    {
        currentFireLifeTime -= Time.deltaTime;
        if (currentFireLifeTime <= 0) EndFire();
    }

    // 돌 사용하여 불 붙일 시 실행
    public void StartFire() 
    {
        isFire = true;
        onFire.SetActive(true);
        offFire.SetActive(false);
        currentFireLifeTime = fireLifeTime;
        transform.GetComponent<SphereCollider>().enabled = false; // 불이 켜진동안에는 추가 상호작용을 못하게한다
    }
        
    // 불이 끝날때
    private void EndFire()
    {
        isFire = false;
        onFire.SetActive(false);
        offFire.SetActive(true);
        currentFireLifeTime = fireLifeTime;
        //Destroy(gameObject, 10f); // 없애는건 너무 잔인하지않아..? ㅋㅋㅋㅋ
        transform.GetComponent<SphereCollider>().enabled = true; // 불이 꺼지면 다시 타오를 수 있게한다.
    }
}
