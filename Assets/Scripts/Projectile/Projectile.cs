using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float speed;
    public float duration;

    public void Launch()
    {
        transform.DOMove(new Vector3(speed * duration * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad), speed * duration * -Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), transform.position.z), duration).onComplete = End;
    }

    public void End()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
