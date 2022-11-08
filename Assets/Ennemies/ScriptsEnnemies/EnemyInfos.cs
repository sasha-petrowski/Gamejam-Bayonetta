using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfos : MonoBehaviour
{
    public int life;
    public float speedPatrol;
    public float speedChase;
    public int damage;
    public float fireRate;
    public float attackRange;
    public float chaseRange;


    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life >= 0)
        {
            Destroy(gameObject);
        }
    }
}

