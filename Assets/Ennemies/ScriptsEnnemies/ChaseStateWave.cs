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
    }

    private void Update()
    {
        if (Vector3.Distance(PlayerPosition.Instance.transform.position, transform.position) <=
            _stateManager.enemyInfos.attackRange)
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
            transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.Instance.transform.position,
                _stateManager.enemyInfos.speedChase * Time.deltaTime);
            return this;
        }
    }
}