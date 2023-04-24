using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Sasum : Prey
{
    protected override void Update()
    {
        base.Update();
        if (theViewAngle.View() && !isDead)
        {
            Run(theViewAngle.GetTargetPos());
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
