using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfos : MonoBehaviour
{
    public int life;
    public float speedPatrol;
    public float speedChase;
    public float fireRate;
    public float attackRange;
    public float chaseRange;
    public int scoreValue;
    public GameObject Impact;
    private GameObject go;

    private bool _dead;

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0 && !_dead)
        {
            _dead = true;
            ParticleSystem system = Impact.GetComponent<ParticleSystem>();
            StartCoroutine(OnDeath(system));
        }
    }

    public IEnumerator OnDeath(ParticleSystem system)
    {
        ScoreManager.AddScore(scoreValue);
        ScoreManager.mult ++;

        go = Instantiate(Impact, transform.position, Quaternion.identity);
        go.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}