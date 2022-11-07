using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    public bool canSeeThePlayer;
    public ChaseState chaseState;
    public List<Transform> patrolsPoints = new List<Transform>();
    private int current;


    private void Start()
    {
        current = 0;
        gameObject.TryGetComponent(out _stateManager);
    }

    private void Update()
    {
        if (Vector3.Distance(PlayerTest.Instance.transform.position, transform.position) <
            _stateManager.enemyInfos.chaseRange)
        {
            canSeeThePlayer = true;
        }
        else
        {
            canSeeThePlayer = false;
        }
    }

    public override State RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            return chaseState;
        }
        else
        {
            if (Vector3.Distance(transform.position, patrolsPoints[current].position) >= 2f)
            {
                //navMesh.SetDestination(patrolsPoints[current].position);
                transform.position = Vector3.MoveTowards(transform.position, patrolsPoints[current].position,
                    _stateManager.enemyInfos.speedPatrol * Time.deltaTime);
            }
            else
            {
                current = (current + 1) % patrolsPoints.Count;
            }

            return this;
        }
    }
}