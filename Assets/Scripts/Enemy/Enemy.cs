using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN;
using RAIN.Perception;
using RAIN.Core;


public abstract class Enemy : Character {
    [SerializeField]
    protected int attackDamage;
    [SerializeField]
    protected string prefabName;
    [SerializeField]
    protected float visualSensorHorizontalAngle, visualSensorVerticalAngle, visualSensorYOffset, visualSensorRange;
    protected string prefabFolder = "Prefabs/Enemy";
    protected AIRig aiRig;

    public void Start()
    {
        transform.parent = null;
        InitializeAI();
    }
    // Update is called once per frame

    public void InitializeAI()
    {
        aiRig = GetComponentInChildren<AIRig>();
        aiRig.AI.Body = gameObject;
        RAIN.Perception.Sensors.VisualSensor visualSensor = new RAIN.Perception.Sensors.VisualSensor();
        visualSensor.CanDetectSelf = false;
        visualSensor.HorizontalAngle = visualSensorHorizontalAngle;
        visualSensor.VerticalAngle = visualSensorVerticalAngle;
        visualSensor.SensorName = "PlayerSensor";
        visualSensor.RequireLineOfSight = true;
        visualSensor.PositionOffset = new Vector3(0, visualSensorYOffset, 0);
        visualSensor.Range = visualSensorRange;
        visualSensor.LineOfSightMask = LayerMask.GetMask("Obstacle", "Enemy", "Player", "Interaction");
        aiRig.AI.Senses.AddSensor(visualSensor);
    }
    private void Update () {
		
	}

    private void OnDestroy()
    {
        foreach (Transform child in gameObject.transform) { Destroy(child.gameObject); };
        Debug.Log(gameObject.transform.name + " killed");
    }
   
    public void ReceiveDamage(int damage, bool critical)
    {
        int tDamage = critical ? (int)Mathf.Ceil(damage * 1.5f) : (int)Mathf.Ceil(damage);

        currentHealth = currentHealth < 0 ? 0 : currentHealth - tDamage;
        
        Debug.Log("Enemy " + gameObject.transform.name + " damaged for "+ tDamage + " | health : "+ currentHealth);
        
        /*if(colorChange != null)
        {
            float greenPercentage = currentHealth / health;
            //Debug.Log(greenPercentage);
            colorChange.ChangeColor(greenPercentage);
        }*/
        CheckDeath();
    }
    
}
