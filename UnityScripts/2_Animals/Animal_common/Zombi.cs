using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombi : Predator
{
    
    private bool isReady = false;
    private float deal = 0;
    private float nextDeal = 100;
    private float stoppingDistance;

    [SerializeField]
    protected ThirdPersonControllerForSurvive targetPlayer;

    public void Start()                                                                     // 플레이어를 바로 타겟 지정
    {
        isZombi = true;
        targetPlayer = GameObject.FindWithTag("Player").GetComponent<ThirdPersonControllerForSurvive>();
    }

    protected override void Update()
    {
        if (isReady)                                                                        // 준비상태가 되면
        {
            base.Update();
            if (!isAttacking && !isDead)
            {
                StopAllCoroutines();
                Chase(targetPlayer.transform.position);                                     // 플레이어 추격
                if (Vector3.Distance(transform.position, destination) <= attackRange)       // 거리가 닿으면 공격 시도
                {
                    StartCoroutine(AttackCoroutine());
                }
            }
        }
        
    }

    public void Rise()                                                                      // 무작위 위치에서 활성화되며, 체력과 공격거리 초기화
    {
        transform.position = RandomPosition();
        currentHp = animalHp;
        stoppingDistance = nav.stoppingDistance;
    }

    public override void Damage(int _dmg, Vector3 _targetPos, int type)
    {
        if (isReady) 
        {
            base.Damage(_dmg, _targetPos,type);
            deal += _dmg;
            try
            {
                nav.ResetPath();
            }
            catch
            {
                transform.position += (Vector3.back * 5);
            }
            if (deal >= nextDeal)                                                           // deal이 nextDeal을 넘기면 Groggy 상태 변경
            {
                anim.SetBool("Groggy", true);
                ZombiGroggy();
                deal = 0;
                nextDeal += 20;                                                            // nextDeal 값 변경
            }
            else
            {
                Chase(targetPlayer.transform.position);
            }
        }
    }

    private void ZombiGroggy()
    {
        isReady = false;                                                                   // 추격 중단
        ZombiAndBGMControl _zombiControl = FindObjectOfType<ZombiAndBGMControl>();
        _zombiControl.SetKillCount();                                                      // 킬카운드 증가
        ResetAction();
        StopAllCoroutines();
        PlaySE(sound_animal_Sleep);
        anim.SetTrigger("Awake");
        RandomSound();
        Chase(targetPlayer.transform.position);                                            // 상태 초기화 후, 일어나는 애니메이션 이후 추격 지속
    }

    
    public void GetDead()
    {
        if (anim.GetBool("Groggy"))                                                        // Groggy 중이라면 바로 비활성화 
        {
            gameObject.SetActive(false);
            return;
        }
        deal = 0;
        isDead = true;
        isReady = false;
        isAction = false;
        isRunning = false;
        isWalking = false;
        isChasing = false;
        isAttacking = false;

        StopAllCoroutines();
        nav.ResetPath();
        StartCoroutine(ZombiDead());                                                       // 아니라면 상태 초기화 후 비활성화 코루틴 실행
    }

    IEnumerator ZombiDead()                                                                // 사망 애니메이션 후 14초 뒤 비활성화 (아침 왔을 때 용)
    {
        anim.SetTrigger("Dead");
        PlaySE(sound_animal_Sleep);
        yield return new WaitForSeconds(14f);
        gameObject.SetActive(false);
    }


    Vector3 RandomPosition()
    {
        ThirdPersonControllerForSurvive _target = GameObject.FindWithTag("Player").GetComponent<ThirdPersonControllerForSurvive>();
        float range_X = _target.GetComponent<CapsuleCollider>().bounds.size.x;
        float range_Z = _target.GetComponent<CapsuleCollider>().bounds.size.z;

        range_X = Random.Range((range_X * 40) * -1, range_X * 40);
        range_Z = Random.Range((range_Z * 40) * -1, range_Z * 40);
        Vector3 RandomPostion = new Vector3(range_X, 5f, range_Z);

        Vector3 respawnPosition = _target.transform.position + RandomPostion;              // 플레이어의 위치를 받아 랜덤 범위 안 값으로 리턴
        return respawnPosition;
    }

    public void Revive_Anim()
    {
        anim.SetBool("Groggy", false);
        isReady = true;
        isDead = false;
        Chase(targetPlayer.transform.position);                                            // 부활 애니메이션 후 추격
    }

    public void CampFire()
    {
        if (!nowFire)
        {
            StartCoroutine(LookFire());                                                    // 코루틴 호출
        }
    }

    private IEnumerator LookFire()
    {
        nowFire = true;
        nav.stoppingDistance = 15f; ;                                                      // 코루틴 동안 정지 거리 증가
        yield return new WaitForSeconds(1f);
        nowFire = false;
        nav.stoppingDistance = stoppingDistance;                                           // 매초 상태 갱신
    }
}
