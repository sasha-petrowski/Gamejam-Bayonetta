using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : ChaseStateWave
{
    public PatrolState patrolState;
    private float distance;
    private void Update()
    {
        distance = Vector3.Distance(PlayerPosition.Instance.transform.position, transform.position);
        if (distance > _stateManager.enemyInfos.chaseRange)
        {
            patrolState.canSeeThePlayer = false;
        }
        if (distance <= _stateManager.enemyInfos.attackRange)
        {
            IsInRange = true;
        }
    }


    public override State RunCurrentState()
    {
        if (patrolState.canSeeThePlayer == false)
        {
            return patrolState;
        }

        if (IsInRange)
        {
            return attackState;
        }
        else
        {
            
            transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.Instance.transform.position,
                _stateManager.enemyInfos.speedChase * Time.deltaTime);
            return this;
        }
    }
}