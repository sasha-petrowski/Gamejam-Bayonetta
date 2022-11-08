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

    public ParticleSystem fxOnShoot;
    public AudioSource soundOnShoot;
    
    public override float Attack()
    {
        float legAngle = Mathf.Atan2(character.legDirection.y, character.legDirection.x) + 0.5f * Mathf.PI;
        Debug.Log(legAngle);
        for (int i = 0; i < projectileAmount; i++)
        {
            fxOnShoot.Play();
            soundOnShoot.Play();

            Projectile newProjectile = GameObject.Instantiate(projectile);
            newProjectile.transform.position = projectileSpawn.transform.position;
            newProjectile.transform.position = projectileSpawn.transform.position;
            newProjectile.transform.rotation = projectileSpawn.transform.rotation;

            float spreadAngle = (projectileSpread * (i / (float)projectileAmount) - projectileSpread / 2f) * Mathf.Deg2Rad;
            
            Debug.Log(spreadAngle);
            
            newProjectile.direction = ( character.legDirection + new Vector3(Mathf.Sin(legAngle + spreadAngle), -Mathf.Cos(legAngle + spreadAngle), 0)) * newProjectile.speed;

            newProjectile.Launch();
        }
        OnAttackEnd();

        return timeBewteenShoots;
    }
}
