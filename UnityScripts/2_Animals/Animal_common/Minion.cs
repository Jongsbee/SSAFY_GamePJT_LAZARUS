using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{

    [SerializeField]
    public int minionHerdNum;
    public Boss thisBoss;
    Animal thisAnimal;
    private int animalRole;
    public float minionHerdRange;

    public bool bossDead = false;

    public void Awake()
    {
        thisAnimal = GetComponent<Animal>();
        thisAnimal.isMinion = true;
        thisAnimal.theMinion = this;
    }

    public void Start()
    {
        if (thisAnimal.isPredator)                      // 종류에 따라 Role 설정
        {
            animalRole = 0;
        }
        else if (thisAnimal.isPrey)
        {
            animalRole = 1;
        }
        else
        {
            animalRole = 2;
        }

    }

    public void SetUp(Boss _Boss, float _herdRange)     // 보스 및 herdRange를 받는 SetUp 메소드
    {
        thisBoss = _Boss;
        minionHerdRange = _herdRange;
    }

    public void SetTarget(Vector3 _targetPos)           // 보스로부터 도망치거나 공격할 타겟을 받는 메소드
    {
        if (!thisAnimal.isSleeping)
        {
            Vector3 _direct = _targetPos + new Vector3(Random.Range(-0.2f, 0.2f), 0f, Random.Range(-0.6f, 0.6f));
            switch (animalRole)
            {
                case 0:
                    GetComponent<Predator>().currentHunger = 0;
                    GetComponent<Predator>().Chase(_direct);
                    break;
                case 1:
                    GetComponent<Prey>().Run(_direct);
                    break;
                case 2:
                    Neutral _Neutral = GetComponent<Neutral>();
                    if (_Neutral.isGroggy)
                    {
                        _Neutral.Run(_direct);
                    }
                    else
                    {
                        _Neutral.Chase(_direct);
                    }

                    break;
            }
        }
        

    }

    public void SetMinionDestination()                  // 이동 시 보스 주변 무작위 좌표로 이동
    {
        if (!thisAnimal.isChasing && !thisAnimal.isAttacking)
        {
            thisAnimal.destination.Set(thisBoss.transform.position.x + Random.Range(-minionHerdRange, minionHerdRange), 0f, thisBoss.transform.position.z + Random.Range(-minionHerdRange * 2f, minionHerdRange * 5f));
        }
    }

    public void TheBossDead(Vector3 _bossPosition)      // 보스 사망 시 굶주림 상태 일시 해제하고 보스 사망 위치로부터 도주
    {
        if (!bossDead)
        {
            bossDead = true;
            if (!thisAnimal.isSleeping)
            {
                switch (animalRole)
                {
                    case 0:
                        Predator _this = GetComponent<Predator>();
                        _this.currentHunger = 30;
                        _this.isHungry = false;
                        _this.Run(_bossPosition);
                        break;
                    case 1:
                        GetComponent<Prey>().Run(_bossPosition);
                        break;
                    case 2:
                        GetComponent<Neutral>().Run(_bossPosition);
                        break;
                }
            }
        }
        else
        {
            bossDead = false;
        }
    }
}
