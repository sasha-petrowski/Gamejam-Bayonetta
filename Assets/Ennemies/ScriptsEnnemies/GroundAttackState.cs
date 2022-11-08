using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttackState : AttackState
{
    public override void Attack()
    {
        int animIndex = Random.Range(0, 1);
        if (animIndex == 0)
        {
            animator.SetBool("Kick", true);
            animator.SetBool("Poing", false);
        }
        if (animIndex == 1)
        {
            animator.SetBool("Poing", true);
            animator.SetBool("Kick", false);
        }
        if (!alreadyAttacked)
        {
            transform.LookAt(Character.Instance.transform);
            Invoke(nameof(ResetAttack), _stateManager.enemyInfos.fireRate);
        }
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
