using System.Collections;
using UnityEngine;

public class Predator : Animal
{
    [SerializeField] protected int attackDamage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackDelay;
    
    [SerializeField] protected float ChaseTime;
    [SerializeField] protected float ChaseDelayTime;
    protected float currentChaseTime;

    [SerializeField] protected LayerMask targetMask;
    [SerializeField] private ParticleSystem hitEffect;

    private void Awake()
    {
        isPredator = true;
    }

    public void Chase(Vector3 _targetPos)
    {
        if (!isDead && !isSleeping)                                                  // 죽지 않고 자고 있지 않을 떄 -> Minion이 죽었을 때에도 함께 쫓는 것을 방지
        {
            if (isBoss)
            {
                theBoss.SendTarget(_targetPos);                                     // Boss의 경우 함께 쫓는 메서드 호출
            }
            destination = _targetPos;                                               
            isAction = true;
            isChasing = true;
            isRunning = true;
            nav.speed = runSpeed;
            nav.SetDestination(destination);                                        // 공격 관련 bool 모두 켜고, 파라미터를 방향으로 설정
        }
    }

    protected IEnumerator ChaseTargetCoroutine()
    {
        currentChaseTime = 0;
        while (currentChaseTime < ChaseTime && isHungry)                            // 배고프고 currentChaseTime이 유효할 때까지만 반복
        {
            if (!theAudio.isPlaying)
            {
                PlaySE(sound_animal_chase);
            }
            
            Chase(theViewAngle.GetTargetPos());                                     // theViewAngle.GetTargetPos의 결과물(사람이나 초식동물)을 쫓도록 함
            if (Vector3.Distance(transform.position, destination) <= attackRange)   // 충분히 가깝고
            {
                if (theViewAngle.View())                                            // 눈 앞에 있을 때
                {
                    if (theViewAngle.lookFood)                                      // 음식이면 Eat 코루틴
                    {
                        StartCoroutine(EatCoroutine());
                    }
                    else if (theViewAngle.lookPrey || theViewAngle.lookPlayer)      // 초식동물이나 사람일 경우 Attack 코루틴
                    {
                        StartCoroutine(AttackCoroutine());
                    }
                }
            }
            yield return new WaitForSeconds(ChaseDelayTime);                        // ChaseDelayTime 마다 반복
            currentChaseTime += ChaseDelayTime;                                     // currentChaseTime에 ChaseDelayTime 추가
        }

        isAction = true;
        isChasing = false;
        isRunning = false;
        nav.ResetPath();                                                            // nav 방향 초기화, 공격 및 추격 상태 초기화
    }

    protected IEnumerator EatCoroutine()
    {
        isAttacking = true;
        transform.LookAt(destination);
        nav.ResetPath();
        nav.speed = 0f;
        currentChaseTime = ChaseTime;                                               // 대상 바라보며 멈추기
        yield return new WaitForSeconds(0.5f);
        yield return null;

        RaycastHit _hit;                                                            // 범위 안에 음식이 있을 시 
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out _hit, attackRange, targetMask))
        {
            if (_hit.transform.tag == "Animal"
                && (_hit.transform.GetComponent<Animal>().isDead && _hit.transform.GetComponent<Animal>().currentHp > 1))
            {
                currentHunger += 40;
                _hit.transform.GetComponent<Animal>().Damage(1, transform.position,2);
                anim.SetTrigger("Eat");
                nav.ResetPath();
                nav.speed = 0f;                                                     // 포만감 수치 증가시키며 타겟인 carcass에 데미지 메소드 호출
            }
        }
        
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        StartCoroutine(ChaseTargetCoroutine());                                     // 포만감 채워질 때까지 식사 지속
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        transform.LookAt(destination);
        nav.ResetPath();
        nav.speed = 0f;
        currentChaseTime = ChaseTime;
        yield return new WaitForSeconds(0.5f);
        yield return null;
        PlaySE(sound_animal_attack);
        anim.SetTrigger("Attack" + Random.Range(0, 8));
        nav.ResetPath();
        nav.speed = 0f;                                                             // 대상 바라보며 멈춘 뒤 공격 시도 애니메이션 재생

        yield return null;

        RaycastHit _hit;                                                            // 범위 안에 타겟이 있을 시
        if (Physics.BoxCast(transform.position + Vector3.up, transform.lossyScale / 2, transform.forward, out _hit, transform.rotation, attackRange, targetMask))
        {
            Instantiate(hitEffect, _hit.point, Quaternion.Euler(0, 90, 0));         // 타격 이펙트 발생
            if (_hit.transform.tag == "Animal"
                && (!_hit.transform.GetComponent<Animal>().isDead))                 // 동물에 데미지 메소드 호출
            {
                _hit.transform.GetComponent<Animal>().Damage(attackDamage, transform.position, 2);
            }
            else if(_hit.transform.tag == "Player"
                && !(StatusController.instance.isDead))
            {
                if (!StatusController.instance.isDown) // 기절한 상태가 아니라면
                {
                    StatusController.instance.DecreaseHP(attackDamage);                 // 사람일 경우 데미지 메소드 호출, 피격 애니메이션 호출.
                    _hit.transform.GetComponent<Animator>().SetTrigger("IsHit");
                    StatusController.instance.isDown = true;
                }

            }
        }
        
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        StartCoroutine(ChaseTargetCoroutine());                                     // 추격 지속
    }

    public override void Damage(int _dmg, Vector3 _targetPos, int type)             // 피격 순간 포만감 수치 초기화 및 추격 시작
    {
        base.Damage(_dmg, _targetPos,type);
        if (!isDead)
        {
            currentHunger = 0;
            Chase(_targetPos);
        }
    }

    
}
