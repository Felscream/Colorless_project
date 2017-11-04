using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float gravityModifier, speed, range;
    [SerializeField]
    protected bool gravityDependant;
    [SerializeField]
    protected string prefab;
    protected float distanceTravelled;
    protected Vector3 lastPos;
    // Use this for initialization
    void Start () {
        //Fast objects may not be destroyed on collision with thin colliders
        lastPos = transform.position;
        distanceTravelled = 0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (gravityDependant)
        {
            Vector3 gravityForce = Physics.gravity * gravityModifier * Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(gravityForce);
        }
	}
    public string GetBulletPrefab()
    {
        return prefab;
    }

    public float GetBulletSpeed()
    {
        return speed;
    }

    public float GetGravityModifier()
    {
        return gravityModifier;
    }

    protected void CheckCollision()
    {
        Vector3 origin = transform.position;
        Vector3 dir = transform.position - lastPos;
        float magnitude = dir.magnitude;
        if (distanceTravelled + dir.magnitude> range)
        {
            magnitude = range - distanceTravelled;
            distanceTravelled = range ; 
            origin = dir.normalized * magnitude;
        }
        else
        {
            distanceTravelled += magnitude;
        }
        RaycastHit hit;
        //check if projectile was supposed to collide with other objects
        if (Physics.Raycast(transform.position, dir, out hit, magnitude, LayerMask.GetMask("Obstacle", "Enemy", "Interaction")))
        {
            ProjectileCollisionReaction(hit);
        }
        lastPos = transform.position;
    }

    protected abstract void ProjectileCollisionReaction(RaycastHit hit);
}
