using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : ChaseStateWave
{
    public PatrolState patrolState;
    
    private void Update()
    {
        if (Vector3.Distance(PlayerTest.Instance.transform.position, transform.position) > _stateManager.enemyInfos.chaseRange)
        {
            patrolState.canSeeThePlayer = false;
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
            //navMesh.SetDestination(PlayerTest.Instance.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, PlayerTest.Instance.transform.position,
                _stateManager.enemyInfos.speedChase * Time.deltaTime);
            return this;
        }
    }
}