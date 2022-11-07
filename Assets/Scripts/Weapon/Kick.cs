using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : Weapon
{
    [Min(0)]
    public float kickTime;

    [SerializeField]
    private Collider _collider;

    public override void OnSelect() 
    {
        _collider.enabled = false;
    }


    public override void Attack()
    {
        base.Attack();
        _collider.enabled = true;
        character.DoExtendLeg(kickTime, retractKick);
    }

    private void retractKick()
    {
        _collider.enabled = false;
        character.DoRetractLeg(kickTime, OnAttackEnd);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Kick : \"{collision.gameObject.name}\"");
    }
}
