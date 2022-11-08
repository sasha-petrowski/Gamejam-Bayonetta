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
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(Character.Instance.transform.position, transform.position) <
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
            animator.SetBool("Ismoving",true);
            return chaseState;
        }
        else
        {
            animator.SetBool("Ismoving",true);
            if (Vector3.Distance(transform.position, patrolsPoints[current].position) >= 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolsPoints[current].position,
                    _stateManager.enemyInfos.speedPatrol * Time.deltaTime);
                transform.LookAt(patrolsPoints[current].position);
            }
            else
            {
                current = (current + 1) % patrolsPoints.Count;
            }

            return this;
        }
    }
}