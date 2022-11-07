using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    public PatrolState patrolState;
    public AttackState attackState;
    public bool IsInRange;
    public float chaseSpeed;

   // public NavMeshAgent navMesh;

    


    private void Update()
    {
        if (Vector3.Distance(PlayerTest.Instance.transform.position, transform.position) > 10f)
        {
            patrolState.canSeeThePlayer = false;
        }
        if (Vector3.Distance(PlayerTest.Instance.transform.position, transform.position) <= 2.5f)
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
            //navMesh.SetDestination(PlayerTest.Instance.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, PlayerTest.Instance.transform.position,
                chaseSpeed * Time.deltaTime);
            return this;
        }
    }
}