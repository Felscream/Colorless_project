﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;
using RAIN.Entities;

public class Brawler : Enemy {
    // Use this for initialization
    [SerializeField]
    private float attackRechargeTime, attackRadius, attackZ, attackY;
    private float lastAttackTime;
    private bool attacking;
    protected new void Awake()
    {
        dead = false;
        currentHealth = BaseHealth;
        lastAttackTime = -attackRechargeTime;
    }

    public IEnumerator Attack()
    {
        Transform self = GetComponent<Transform>();
        animator.SetBool("attack", true);
        
        Debug.Log("Winding up attack");
        Debug.Log("Dealing damage");
        Vector3 detectorLocation = self.position + new Vector3(0, attackY, attackZ);
        Collider[] detector = Physics.OverlapSphere(detectorLocation, attackRadius, LayerMask.GetMask("Player"));
        Debug.Log(detector.Length);
        if(detector.Length > 0)
        {
            Player.GetInstance().ReceiveDamage(attackDamage);
             
        }
        lastAttackTime = Time.time;
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("attack", false);
    }

    protected override void InitializeAI()
    {
        aiRig = GetComponentInChildren<AIRig>();
        aiRig.AI.Body = this.gameObject;
        entityRig = GetComponentInChildren<EntityRig>();
        entityRig.Entity.Form = this.gameObject;
        aiRig.AI.Motor.Speed = speed;
        aiRig.AI.Motor.CloseEnoughDistance = closeDistance;
        RAIN.Perception.Sensors.VisualSensor visualSensor = new RAIN.Perception.Sensors.VisualSensor
        {
            IsActive = true,
            CanDetectSelf = false,
            HorizontalAngle = visualSensorHorizontalAngle,
            VerticalAngle = visualSensorVerticalAngle,
            SensorName = "PlayerSensor",
            RequireLineOfSight = true,
            PositionOffset = new Vector3(0, visualSensorYOffset, 0),
            Range = visualSensorRange,
            LineOfSightMask = LayerMask.GetMask("Obstacle", "Enemy", "Player", "Interaction")
        };
        RAIN.Perception.Sensors.VisualSensor enemySensor = new RAIN.Perception.Sensors.VisualSensor
        {
            IsActive = true,
            CanDetectSelf = false,
            HorizontalAngle = 180,
            VerticalAngle = 40,
            SensorName = "EnemySensor",
            RequireLineOfSight = true,
            PositionOffset = new Vector3(0, visualSensorYOffset, 0),
            Range = 11,
            LineOfSightMask = LayerMask.GetMask("Obstacle", "Enemy")
        };
        RAIN.Entities.Aspects.VisualAspect visualAspect = new RAIN.Entities.Aspects.VisualAspect
        {
            AspectName = "Enemy",
            IsActive = true,
            MountPoint = gameObject.transform,
            Position = new Vector3(0,1,0),
            VisualSize = 1
        };
        aiRig.AI.Senses.AddSensor(visualSensor);
        aiRig.AI.Senses.AddSensor(enemySensor);
        //entityRig.Entity.AddAspect(visualAspect);
    }
    protected override IEnumerator Die()
    {
        animator.SetBool("dead", true);
        lifeGemCreator.GetComponent<LifeGemSpawn>().SpawnLifeGem();
        yield return new WaitForSeconds(2.0f);
        aiManager.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    public void DropLifeGems()
    {
        //int lifeGemToDrop = Random.Range(minLifeGemDropped, maxLifeGemDropped);
    }
    public float GetLastAttackTime()
    {
        return lastAttackTime;
    }
}
