using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public abstract class HitScan : Weapon {
    [SerializeField]
    protected float weaponRange;
    [SerializeField]
    protected int damage, startingBulletSpread, maxBulletSpread,
        bulletSpreadRatePerSecond, bulletSpreadRecovery;
    protected float bulletSpread, spreading;
    protected LineRenderer laserLine;

    protected abstract IEnumerator ShotEffect();

    private new void Start () {
        base.Start();
        laserLine = GetComponent<LineRenderer>();
        if (startingBulletSpread > maxBulletSpread || startingBulletSpread<0)
        {
            Debug.Log("Bullet spreading error on " + gameObject.transform.name);
        }
        bulletSpread = startingBulletSpread;
    }


    public void IncrementBulletSpread()
    {
        bulletSpread += bulletSpreadRatePerSecond * Time.deltaTime;
        if(bulletSpread > maxBulletSpread)
        {
            bulletSpread = maxBulletSpread;
        }
        //Debug.Log(bulletSpread);
    }

    public void ResetBulletSpread()
    {
        bulletSpread = startingBulletSpread;
    }

    public void DecrementBulletSpread()
    {
        bulletSpread -= bulletSpreadRatePerSecond * bulletSpreadRecovery * Time.deltaTime;
        if (bulletSpread < startingBulletSpread)
        {
            bulletSpread = startingBulletSpread;
        }
        //Debug.Log(bulletSpread);
    }

    public override void Fire()
    {
        //Debug.Log(Time.time+" "+ firingStart);
        if (CanFire())
        {
            Debug.Log("Firing");
            firingStart = Time.time;
            Vector3 dir = cam.transform.forward;
            StartCoroutine(ShotEffect());
            spreading = Random.Range(startingBulletSpread, bulletSpread);
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            // Set the start position for our visual effect for our laser to the position of gunEnd
            laserLine.SetPosition(0, bulletSpawn.position);

             //apply bullet spreading
            float accuracy = spreading / maxBulletSpread;
            Vector3 randomOffset = new Vector3()
            {
                x = Random.Range(-(0.1f * accuracy), 0.1f * accuracy),
                y = Random.Range(-(0.1f * accuracy), 0.1f * accuracy),
                z = 0
            };
            dir += randomOffset;
            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, dir, out hit, weaponRange, LayerMask.GetMask("Obstacle","Enemy","Interaction")))
            {
                laserLine.SetPosition(1, hit.point);
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (hit.collider.gameObject.CompareTag("Enemy_head"))
                    {
                        hit.collider.GetComponentInParent<Enemy>().ReceiveDamage(damage, true);
                    } 
                    else
                    {
                        hit.collider.GetComponentInParent<Enemy>().ReceiveDamage(damage, false);
                    }
                }
            }
            else
            {
                // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                laserLine.SetPosition(1, rayOrigin + (dir * weaponRange));
            }
            CameraRecoil();
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
            if (!(Time.time >= (1 / fireRate + firingStart)))
            {
                Debug.Log("Blocked by rate of fire");
            }
            else{
                Debug.Log("What ?");
            }
                

        }
    }

    public float GetSpreading()
    {
        return spreading;
    }
}
