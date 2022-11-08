using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseStateWave : State
{
    public AttackState attackState;
    public bool IsInRange;

    private void Start()
    {
        gameObject.TryGetComponent(out _stateManager);
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(Character.Instance.transform.position, transform.position) <=
            _stateManager.enemyInfos.attackRange )
        {
            IsInRange = true;
        }
    }


    public override State RunCurrentState()
    {
        if (IsInRange)
        {
            return attackState;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Character.Instance.transform.position + new Vector3(0f,-3f,0f),
                _stateManager.enemyInfos.speedChase * Time.deltaTime);
            return this;
        }
    }
}