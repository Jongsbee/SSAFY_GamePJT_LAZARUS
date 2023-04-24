using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Animal
{
    private void Start()
    {
        isPrey = true;
    }

    public void Run(Vector3 _targetPos)
    {
        destination = new Vector3(transform.position.x - _targetPos.x, 0f, transform.position.z - _targetPos.z).normalized;

        currentTime = RunTime;
        isWalking = false;
        isRunning = true;
        nav.speed = runSpeed;
        anim.SetBool("Running", isRunning);
    }

    public override void Damage(int _dmg, Vector3 _targetPos)
    {
        Debug.Log("¾Æ¾ß!");
        Debug.Log(currentHp);
        base.Damage(_dmg, _targetPos);
        if (!isDead)
        {
            Run(_targetPos);
        }
    }
}
