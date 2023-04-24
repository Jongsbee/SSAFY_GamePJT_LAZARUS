using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baam : neutral
{
    protected override void Update()
    {
        base.Update();
        if (theViewAngle.View() && !isDead && !isAttacking)
        {
            if (currentHp < (animalHp / 2))
            {
                Run(theViewAngle.GetTargetPos());
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ChaseTargetCoroutine());
            }
            
        }
    }

    protected override void ResetAction()
    {
        base.ResetAction();
        RandomAction();
    }

    private void RandomAction()
    {

        //RandomSound();
        int _random = Random.Range(0, 5);

        if (_random == 0)
        {
            Wait();
        }
        else if (_random == 1)
        {
            Eat();
        }
        else if (_random == 2)
        {
            Peek();
        }
        else if (_random == 3)
        {
            Sit();
        }
        else if (_random == 4)
        {
            TryWalk();
        }
    }
}
