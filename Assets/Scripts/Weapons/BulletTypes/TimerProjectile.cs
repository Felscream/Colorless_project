using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerProjectile : Bullet {
    [SerializeField]
    private float timeToLive, explosionRadius;
    private float timeElapsed;
	// Use this for initialization
	void Start () {
        timeElapsed = 0f;
	}

    // Update is called once per frame
    private void Update()
    {
        CheckCollision();
        CheckTimer();
    }


    protected override void ProjectileCollisionReaction(RaycastHit hit)
    {
        Collider[] detector = Physics.OverlapSphere(transform.position, explosionRadius, LayerMask.GetMask("Interaction","Enemy","Obstacle"));
        foreach(Collider it in detector)
        {
            if(it.gameObject.layer == LayerMask.NameToLayer("Enemy") && !it.gameObject.CompareTag("Enemy_head"))
            {
                it.GetComponentInParent<Enemy>().ReceiveDamage(damage, false);
                Destroy(gameObject);
            }
        }
    }

    private void CheckTimer()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= timeToLive)
        {
            Collider[] detector = Physics.OverlapSphere(transform.position, explosionRadius, LayerMask.GetMask("Enemy"));
            foreach (Collider it in detector)
            {
                it.GetComponentInParent<Enemy>().ReceiveDamage(damage, false);
            }
            Destroy(gameObject);
        }
    }
}
