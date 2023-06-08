using System.Collections;
using UnityEngine;

public class Prey : Animal
{
    [SerializeField]
    protected LayerMask targetMask;

    [SerializeField]
    protected float EatingTime;
    protected float currentEatingTime;

    private void Awake()
    {
        isPrey = true;
    }

    protected override void Update()
    {
        base.Update();
        if (theViewAngle.View()                         // 배고프고 풀을 봤다면
            && theViewAngle.lookFood 
            && !isDead 
            && isHungry
            && !isAttacking)
        {
            StopAllCoroutines();
            StartCoroutine(ChaseTargetCoroutine());     // 식사 코루틴 시작 -> 현재 맵 상에 Grass 오브젝트강 없으므로 사용되고 있지 않은 코드.
        }

        else if (theViewAngle.View() 
                && (theViewAngle.lookPredator || theViewAngle.lookPlayer) 
                && !isDead)
        {
            theViewAngle.lookFood = false;
            StopAllCoroutines();
            Run(theViewAngle.GetTargetPos());           // 포식동물이나 플레이어 발견시 도주 코루틴 실행
        }
    }

    

    public void BackToBoss(Vector3 _targetPos)          // Minion이라면 Herd의 Boss에로 돌아가도록 하는 메소드
    {
        if (!isDead)
        {
            destination = _targetPos;
            currentTime = RunTime;
            isAction = true;
            isWalking = false;
            isRunning = true;
            nav.speed = runSpeed;
        }
    }

    public override void Damage(int _dmg, Vector3 _targetPos, int type)
    {
        base.Damage(_dmg, _targetPos,2);
        if (!isDead)
        {
            currentHunger = animalHunger;               // 피격 시 포만감 최대치로 채우며 도주 코루틴 실행
            Run(_targetPos);
        }
    }

    public void Chase(Vector3 _targetPos)
    {
        destination = _targetPos;

        nav.SetDestination(destination);

        isAction = true;
        isWalking = true;
        nav.speed = walkSpeed;                          // 파라미터로 받은 타겟위치로 이동
    }

    protected IEnumerator ChaseTargetCoroutine()
    {
        currentEatingTime = 0;
        while (isHungry)
        {
            isChasing = true;
            Chase(theViewAngle.GetFoodPos());
            if (Vector3.Distance(transform.position, theViewAngle.GetFoodPos()) <= 7f) // 충분히 가깝고
            {
                if (theViewAngle.View())                // 눈 앞에 있을 때
                {
                    isWalking = false;
                    anim.SetBool("Walking", isWalking);
                    StartCoroutine(AttackCoroutine());  // 식사 코루틴 실행
                }
            }

            yield return new WaitForSeconds(5f);
        }

        isAction = true;
        isWalking = true;
        isChasing = false;
        theViewAngle.lookFood = false;
        nav.ResetPath();
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        nav.ResetPath();
        yield return new WaitForSeconds(0.5f);
        transform.LookAt(theViewAngle.GetFoodPos());

        anim.SetTrigger("Eat");
        yield return null;
        RaycastHit _hit;

        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out _hit, 7, targetMask))
        {
            if (_hit.transform.tag == "Grass")
            {
                _hit.transform.GetComponent<Grass>().Damage(1, transform.position);
                currentHunger += 50f;
            }
        }
        
        yield return new WaitForSeconds(6f);
        isAttacking = false;
        StartCoroutine(ChaseTargetCoroutine());         // 레이캐스트에 감지된 Grass 오브젝트에 데미지 전달하며 포만감 수치 증가
    }
}
