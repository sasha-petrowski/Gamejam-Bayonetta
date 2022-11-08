using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject prefabBullet;
    public Vector3 offsetBullet;
    
    public void AttackFly()
    {
        GameObject go = Instantiate(prefabBullet, transform.position + offsetBullet, Quaternion.identity);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 12f, ForceMode.Impulse);
    }
}
