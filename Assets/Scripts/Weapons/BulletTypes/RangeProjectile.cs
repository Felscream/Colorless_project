using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeProjectile : Bullet {

    private void Update()
    {
        CheckCollision();
        CheckDistanceTravelled();
    }

    protected override void ProjectileCollisionReaction(RaycastHit hit)
    {
        Debug.Log(hit.collider.gameObject.name);
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
            Destroy(gameObject);
        }
        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }

    }
    private void CheckDistanceTravelled()
    {
        if (distanceTravelled >= range)
        {
            Destroy(gameObject);
        }
    }

}
