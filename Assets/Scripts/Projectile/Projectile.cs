using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public float duration;
    private EnemyInfos enemy;

    [HideInInspector]
    public Vector3 direction;

    private float _timeAtLaunch;

    private bool _moving = false;

    private void Update()
    {
        if (_moving)
        {
            if (Time.time < _timeAtLaunch + duration)
            {
                transform.position += direction * Time.deltaTime;
            }
            else
            {
                End();
            }
        }
    }

    public void Launch()
    {
        _moving = true;
        _timeAtLaunch = Time.time;
    }

    public void End()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        enemy = other.gameObject.GetComponentInParent<EnemyInfos>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
