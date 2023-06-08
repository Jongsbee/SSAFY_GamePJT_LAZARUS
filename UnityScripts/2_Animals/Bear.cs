using UnityEngine;

public class Bear : Predator
{
    protected override void Update()
    {
        base.Update();
        if (theViewAngle.View() 
            && !isDead 
            && !isAttacking)
        {
            StopAllCoroutines();
            StartCoroutine(ChaseTargetCoroutine());
        }
    }

    protected override void ResetAction()
    {
        base.ResetAction();
        if (!isSleeping)
        {
            RandomAction();
        }
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
            TryWalk();
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
