using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral : Animal
{
    [SerializeField] protected int attackDamage;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected LayerMask targetMask;

    public bool isGroggy;
    private bool isAvenge;

    [SerializeField] protected float ChaseTime;
    [SerializeField] protected float ChaseDelayTime;
    protected float currentChaseTime;

    [SerializeField] private ParticleSystem hitEffect;

    protected override void Update()
    {
        base.Update();
        if (theViewAngle.View() && isGroggy  && !isDead)                            // 일정 체력 수치 이하일 경우 도주
        {
            StopAllCoroutines();
            Run(theViewAngle.GetTargetPos());
            //isAction = false;
        }
        else if (theViewAngle.View() && isAvenge && !isDead && !isAttacking)        // 일정 체력 수치 이상일 경우 추격
        {
            StopAllCoroutines();
            StartCoroutine(ChaseTargetCoroutine());
        }
    }

    public void Chase(Vector3 _targetPos)
    {
        if (!isDead && !isSleeping)
        {
            if (isBoss)
            {
                theBoss.SendTarget(_targetPos);
            }
            destination = _targetPos;
            isAction = true;
            isChasing = true;
            nav.speed = runSpeed;
            nav.SetDestination(destination);                                        // 전달받은 타겟 방향을 runSpeed로 이동
        }

    }
    protected IEnumerator ChaseTargetCoroutine()
    {
        currentChaseTime = 0;
        while (currentChaseTime < ChaseTime)
        {
            if (!theAudio.isPlaying)
            {
                PlaySE(sound_animal_chase);
            }
            Chase(theViewAngle.GetTargetPos());
            if (Vector3.Distance(transform.position, theViewAngle.GetTargetPos()) <= 3f) // 충분히 가깝고
            {
                if (theViewAngle.View())                                            // 눈 앞에 있을 때
                {
                    StartCoroutine(AttackCoroutine());
                }
            }

            yield return new WaitForSeconds(ChaseDelayTime);
            currentChaseTime += ChaseDelayTime;
        }
        isAction = true;
        isPredator = false;
        isChasing = false;
        isRunning = false;
        nav.ResetPath();                                                            // 상태 초기화
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        nav.ResetPath();
        currentChaseTime = ChaseTime;
        yield return new WaitForSeconds(0.5f);
        transform.LookAt(destination);

        anim.SetTrigger("Attack" + Random.Range(0, 8));
        yield return null;
        
        RaycastHit _hit;                                                            // 박스캐스트에 감지된 오브젝트에 따라 데미지 메소드 호출
        if (Physics.BoxCast(transform.position + Vector3.up, transform.lossyScale / 2, transform.forward, out _hit, transform.rotation, 3, targetMask))
        {
            Instantiate(hitEffect, _hit.point, Quaternion.Euler(0, 90, 0));
            if (_hit.transform.tag == "Animal"
                && (!_hit.transform.GetComponent<Animal>().isDead))
            {
                _hit.transform.GetComponent<Animal>().Damage(attackDamage, transform.position,2);
            }
            else if (_hit.transform.tag == "Player"
                && !(StatusController.instance.isDead))
            {
                StatusController.instance.DecreaseHP(attackDamage);
                _hit.transform.GetComponent<Animator>().SetTrigger("IsHit");
                StatusController.instance.isDown = true;
            }
        }
        yield return new WaitForSeconds(attackDelay);                       // 상태 초기화
        isAttacking = false;
        StartCoroutine(ChaseTargetCoroutine());                             // 추격 지속
    }

    

    public override void Damage(int _dmg, Vector3 _targetPos, int type)
    {
        base.Damage(_dmg, _targetPos,type);
        if (currentHp < (animalHp/2) )
        {
            isChasing = false;
            isPredator = false;
            isPrey = true;
            isAttacking = false;
            isGroggy = true;
            isAvenge = false;
            StopAllCoroutines();
            Run(_targetPos);                                                // 체력의 반 이하일 경우 IsGroggy 켜고 도주 메소드 실행
        }
        else
        {
            isAvenge = true;
            isPredator = true;
            Chase(_targetPos);                                              // 그 전까지는 추격 실행
        }
    }
}
