using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackState : AttackState
{
    public override void Attack()
    {
        if (!alreadyAttacked)
        {
            animator.SetBool("Attack",true);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _stateManager.enemyInfos.fireRate);
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}