using UnityEngine;

public class Foot : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem footPrint;

    [SerializeField]
    private float height;

    private bool isSet = false;
    [SerializeField]
    private Animal thisAnimal;

    private void Start()
    {
        height = transform.localPosition.y;               // 활성화 당시 y 포지션 위치를 기억
    }

    public void SetUp(Animal _thisAnimal)
    {
        thisAnimal = _thisAnimal;
        isSet = true;
    }

    //private void Update()
    //{
    //    //if (isSet && thisAnimal.GetIsRunning() && transform.localPosition.y < height)
    //    if (isSet && thisAnimal.GetIsRunning())           // 달릴 때, 기억된 포지션보다 y포지션이 높아지면 인스턴스 발생 (현재 최적화를 위해 주석처리)
    //    {
    //        Instantiate(footPrint, transform.position, Quaternion.identity);
    //    }
    //}
}
