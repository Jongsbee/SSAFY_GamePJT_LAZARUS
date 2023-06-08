using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] public int herdNum;

    [SerializeField] private float herdRange;

    [SerializeField] List<Minion> herd;
    

    public void Awake()
    {
        GetComponent<Animal>().isBoss = true;
        GetComponent<Animal>().theBoss = this;
        foreach (Minion minion in FindObjectsOfType(typeof(Minion)))
        { 
            if (minion.minionHerdNum == herdNum)
            {
                herd.Add(minion);
                minion.SetUp(this, herdRange);
            }                                       // herdNum이 같을 경우 minion에 넣고 herdRange 부여
        }
        
    }
    public void SendTarget(Vector3 _targetPos)
    {
        foreach(Minion minion in herd)
        {
            try
            {
                minion.SetTarget(_targetPos);       // 살아 있는 minion에 타겟 좌표 파라미터로 메소드 실행
            }
            catch
            {
                Debug.Log("죽은 미니언");
            }   
        }
    }

    public void SendDestination()
    {
        foreach (Minion minion in herd)
        {
            try
            {
                minion.SetMinionDestination();       // 살아 있는 minion에 복귀 메소드 실행
            }
            catch
            {
                Debug.Log("죽은 미니언");
            }
            
        }
    }

    public void BossDead()
    {
        foreach (Minion minion in herd)
        {
            try
            {
                minion.TheBossDead(transform.position); // 사망 위치를 좌표로 메소드 실행
            }
            catch
            {
                Debug.Log("죽은 미니언");
            }
            
        }
    }
}
