using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : ChaseStateWave
{
    public PatrolState patrolState;
    private float distance;
  
    private void Update()
    {
        distance = Vector3.Distance(Character.Instance.transform.position, transform.position);
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
            animator.SetBool("Ismoving",true);
            return patrolState;
        }

        if (IsInRange)
        {
            animator.SetBool("Ismoving",false);
            return attackState;
        }
        else
        {
            animator.SetBool("Ismoving",true);
            transform.position = Vector3.MoveTowards(transform.position, Character.Instance.transform.position + new Vector3( 0f,-3f,0f),
                _stateManager.enemyInfos.speedChase * Time.deltaTime);
            return this;
        }
    }
}