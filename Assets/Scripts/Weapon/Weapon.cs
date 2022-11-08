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

    public virtual float Attack() { return 0f; }

    public virtual void OnAttackEnd()
    {
        character.OnAttackEnd();
    }
}
