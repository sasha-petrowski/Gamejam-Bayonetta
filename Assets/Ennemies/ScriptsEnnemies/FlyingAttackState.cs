using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackState : AttackState
{
    public GameObject prefabBullet;
    private bool alreadyAttacked = false;


    public override void Attack()
    {
        if (!alreadyAttacked)
        {
            transform.LookAt(PlayerTest.Instance.transform);
            GameObject go = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _stateManager.enemyInfos.fireRate);
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}