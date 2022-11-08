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
        character.DoRetractLeg(kickTime, null);
    }
    public override void OnQuit()
    {
        character.DoExtendLeg(kickTime, null);
    }


    public override float Attack()
    {
        _collider.enabled = true;
        character.DoExtendLeg(kickTime, retractKick);
        return kickTime * 2;
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
