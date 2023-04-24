    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Animal
{
    [SerializeField]
    protected int attackDamage;
    [SerializeField]
    protected float attackDelay;
    [SerializeField]
    protected LayerMask targetMask;

    [SerializeField]
    protected float ChaseTime;
    protected float currentChaseTime;
    [SerializeField]
    protected float ChaseDelayTime;

    private void Start()
    {
        isPredetor = true;
    }

    public void Chase(Vector3 _targetPos)
    {
        destination = _targetPos;
        isChasing = true;
        nav.speed = runSpeed;
        isRunning = true;
        anim.SetBool("Running", isRunning);
        nav.SetDestination(destination);

    }
    protected IEnumerator ChaseTargetCoroutine()
    {
        currentChaseTime = 0;
        while (currentChaseTime < ChaseTime)
        {
            Chase(theViewAngle.GetTargetPos());
            if (Vector3.Distance(transform.position, theViewAngle.GetTargetPos()) <= 7f) // 충분히 가깝고
            {
                if (theViewAngle.View()) // 눈 앞에 있을 때
                {
                    Debug.Log("맹수가 공격을 시도한다!");
                    StartCoroutine(AttackCoroutine());
                }
            }

            yield return new WaitForSeconds(ChaseDelayTime);
            currentChaseTime += ChaseDelayTime;
        }
        isChasing = false;
        isRunning = false;
        anim.SetBool("Running", isRunning);
        nav.ResetPath();
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        nav.ResetPath();
        currentChaseTime = ChaseTime;
        yield return new WaitForSeconds(0.5f);
        transform.LookAt(theViewAngle.GetTargetPos());

        //Debug.Log(theViewAngle.GetTargetPos());
        anim.SetTrigger("Attack");
        yield return null;
        RaycastHit _hit;
        
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out _hit, 10, targetMask))
        {
            Debug.Log("맞았다!");
            if (_hit.transform.tag == "Animal")
            {
                _hit.transform.GetComponent<Prey>().Damage(attackDamage, transform.position);
            }
        }
        else
        {
            Debug.Log("빗나갔다!");
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        StartCoroutine(ChaseTargetCoroutine());
    }

    public override void Damage(int _dmg, Vector3 _targetPos)
    {
        base.Damage(_dmg, _targetPos);
        if (!isDead)
        {
            Chase(_targetPos);
        }
    }
}
