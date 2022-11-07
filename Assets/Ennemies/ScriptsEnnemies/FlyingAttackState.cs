using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackState : State
{
    public ChaseState chaseState;

    private void Update()
    {
        if (Vector3.Distance(PlayerTest.Instance.transform.position, transform.position) >=2.5f)
        {
            chaseState.IsInRange = false;
        }
    }


    public override State RunCurrentState()
    {
        if (chaseState.IsInRange == false)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
