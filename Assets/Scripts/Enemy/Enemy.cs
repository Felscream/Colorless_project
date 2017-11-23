using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN;
using RAIN.Perception;
using RAIN.Core;
using RAIN.Entities;



public abstract class Enemy : Character
{
	[SerializeField]
	protected int attackDamage, minLifeGemDropped, maxLifeGemDropped, minLifeGemValue, maxLifeGemValue;
	[SerializeField]
	protected string prefabName;
	[SerializeField]
	protected float visualSensorHorizontalAngle, visualSensorVerticalAngle, visualSensorYOffset, visualSensorRange, speed, stepOffset, closeDistance;
	protected Rigidbody rb;
	protected AIManager aiManager;
	protected string prefabFolder = "Prefabs/Enemy";
	protected AIRig aiRig;
	protected EntityRig entityRig;
	protected Transform lifeGemCreator;
	private ColoriseRoom coloriseRoom;
	private float colorRatio;


	public void Start()
	{
		coloriseRoom = GetComponentInParent<EnemySpawner>().GetRoom();
		colorRatio = GetComponentInParent<EnemySpawner>().GetColorRatio();
		transform.parent = null;
		aiManager = GameObject.FindGameObjectWithTag("AIManager").GetComponent<AIManager>();
		aiManager.AddEnemy(gameObject);
		rb = GetComponent<Rigidbody>();
		InitializeAI();
		lifeGemCreator = transform.Find("LifeGemSpawn");

	}
	// Update is called once per frame

	
	private void Update()
	{

	}

	private void OnDestroy()
	{
		coloriseRoom.ColoriseRoomTexture(colorRatio);

		foreach (Transform child in gameObject.transform) { Destroy(child.gameObject); };
		Debug.Log(gameObject.transform.name + " killed");
	}

	public void ReceiveDamage(int damage, bool critical)
	{
		int tDamage = critical ? (int)Mathf.Ceil(damage * 1.5f) : (int)Mathf.Ceil(damage);

		currentHealth = currentHealth < 0 ? 0 : currentHealth - tDamage;

		Debug.Log("Enemy " + gameObject.transform.name + " damaged for " + tDamage + " | health : " + currentHealth);

		/*if(colorChange != null)
        {
            float greenPercentage = currentHealth / health;
            //Debug.Log(greenPercentage);
            colorChange.ChangeColor(greenPercentage);
        }*/
		CheckDeath();
	}

	public void OnCollisionEnter(Collision collision)
	{
		Collider myCollider = GetComponent<Collider>();
		if (collision.gameObject.layer == LayerMask.GetMask("Obstacle"))
		{
			Debug.Log("col");
			foreach (ContactPoint cp in collision.contacts)
			{
				if (cp.thisCollider == myCollider)
				{
					if (cp.point.y < stepOffset && cp.point.y > myCollider.bounds.min.y)
					{
						//step up
						transform.position = Vector3.MoveTowards(transform.position, cp.point, Time.deltaTime * speed);
						rb.velocity = transform.up;
					}
				}
			}
		}

	}
}
