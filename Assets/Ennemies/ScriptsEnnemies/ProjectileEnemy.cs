using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public int damage;
    public int duration;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Character>())
         {
             Character.Instance.TakeDamage(damage);
             Destroy(gameObject);
         }
    }

    private void Start()
    {
        StartCoroutine(WaitBeforeDestroy());
    }

    private IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
