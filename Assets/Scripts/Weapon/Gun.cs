using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public float timeBewteenShoots;

    public Transform projectileSpawn;
    public Projectile projectile;
    [Min(1)]
    public int projectileAmount;
    [Tooltip("Angle between the first and last projectile")]
    public float projectileSpread;



    private float _timeAtLastShot;
    
    public override void Attack()
    {
        base.Attack();
        if(Time.time > _timeAtLastShot + timeBewteenShoots)
        {
            _timeAtLastShot = Time.time;
            
            for(int i = 0; i < projectileAmount; i++)
            {
                Projectile newProjectile = GameObject.Instantiate(projectile);
                newProjectile.transform.position = projectileSpawn.transform.position;
                newProjectile.transform.rotation = projectileSpawn.transform.rotation;
                newProjectile.transform.Rotate(new Vector3(0, 0, projectileSpread * (i / (float)projectileAmount) - projectileSpread / 2f));
                newProjectile.Launch();
            }

            OnAttackEnd();
        }
    }
}
