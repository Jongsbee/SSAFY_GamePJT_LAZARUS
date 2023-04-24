using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neutral : Animal
{
    [SerializeField]
    protected float attackDamage;
    [SerializeField]
    protected float attackDelay;
    [SerializeField]
    protected LayerMask targetMask;

    [SerializeField]
    protected float ChaseTime;
    protected float currentChaseTime;
    [SerializeField]
    protected float ChaseDelayTime;

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
            if (Vector3.Distance(transform.position, theViewAngle.GetTargetPos()) <= 3f) // ����� ������
            {
                if (theViewAngle.View()) // �� �տ� ���� ��
                {
                    Debug.Log("�ͼ��� ������ �õ��Ѵ�!");
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
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        RaycastHit _hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out _hit, 3, targetMask))
        {
            Debug.Log("�¾Ҵ�!");
        }
        else
        {
            Debug.Log("��������!");
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        StartCoroutine(ChaseTargetCoroutine());
    }

    public void Run(Vector3 _targetPos)
    {
        destination = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;

        currentTime = RunTime;
        isWalking = false;
        isRunning = true;
        nav.speed = runSpeed;
        anim.SetBool("Running", isRunning);
    }

    public override void Damage(int _dmg, Vector3 _targetPos)
    {
        base.Damage(_dmg, _targetPos);
        if (!isDead && currentHp < (animalHp/2) )
        {
            isChasing = false;
            isAttacking = false;
            StopAllCoroutines();
            Run(_targetPos);
        }
        else
        {
            Chase(_targetPos);
        }
    }
}
