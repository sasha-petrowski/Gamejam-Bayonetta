using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : State
{
    public ChaseStateWave chaseState;
    protected bool alreadyAttacked = false;

    private void Start()
    {
        gameObject.TryGetComponent(out _stateManager);
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(Character.Instance.transform.position, transform.position) >=
            _stateManager.enemyInfos.attackRange)
        {
            transform.LookAt(Character.Instance.transform);
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