using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public Character character;
    [HideInInspector]
    public bool isAttacking;

    public virtual void OnSelect() { }

    public virtual void Attack()
    {
        isAttacking = true;
    }

    public virtual void OnAttackEnd()
    {
        isAttacking = false;
        character.OnAttackEnd();
    }
}
