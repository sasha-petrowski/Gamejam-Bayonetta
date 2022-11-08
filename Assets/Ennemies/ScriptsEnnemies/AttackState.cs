using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : State
{
    public ChaseStateWave chaseState;

    private void Start()
    {
        gameObject.TryGetComponent(out _stateManager);
    }
    
    private void Update()
    {
        if (Vector3.Distance(PlayerPosition.Instance.transform.position, transform.position) >= _stateManager.enemyInfos.attackRange)
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
            Attack();            
            return this;
        }
    }

    public abstract void Attack();
}
