using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon {

    [SerializeField]
    protected GameObject projectile;
    protected float bulletSpeed, gravityModifier;
    // Use this for initialization
    new void Start () {
        base.Start();
        if (ProjectileSelected())
        {
            bulletSpeed = projectile.GetComponent<Bullet>().GetBulletSpeed();
            gravityModifier = projectile.GetComponent<Bullet>().GetGravityModifier();
            UpdateAmmoInfo();
        }
        else
            Debug.Log("No projectile for this weapon");
    }
	
    public override void Fire()
    {
        if (ProjectileSelected())
        {
            //Debug.Log(Time.time+" "+ firingStart);
            if (CanFire())
            {
                firingStart = Time.time;
                GameObject bullet = Instantiate(projectile, bulletSpawn.position, bulletSpawn.rotation);
                Vector3 targetDirection;
                RaycastHit hit;
                Vector3 dir = cam.transform.forward;
                Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                if (Physics.Raycast(rayOrigin, dir, out hit, 50, LayerMask.GetMask("Obstacle", "Enemy", "Interaction")))
                {
                    targetDirection = (hit.point - bulletSpawn.position).normalized;
                }
                else
                {
                    float x = cam.pixelWidth / 2;
                    float y = cam.pixelHeight / 2;
                    targetDirection = (cam.ScreenToWorldPoint(new Vector3(x, y, 50f)) - bulletSpawn.position).normalized;
                }
                Vector3 force = targetDirection * bulletSpeed;
                    /*Vector3 destination = cam.ScreenToWorldPoint(new Vector3(x, y, 0.0f));
                    Debug.Log(destination);
                    Vector3 dir = (destination - bulletSpawn.transform.position).normalized;*/
                    
                bullet.GetComponent<Rigidbody>().AddForce(force);
                weaponData.DecrementClipAmmo();
                UpdateAmmoInfo();
                //Automatic reload
                if (weaponData.GetClipAmmo() <= 0)
                {
                    StartCoroutine(Reload());
                }
            }
            else
            {
                /*if (weaponData.GetClipAmmo() <= 0)
                    Debug.Log("no ammo");
                else
                    Debug.Log("What ?");*/

            }
        }
        else
        {
            Debug.Log("No projectile for this weapon");
        }
    }

    private bool ProjectileSelected()
    {
        if (projectile != null)
        {
            return true;
        }
        return false;
    }
    
}
