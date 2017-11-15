using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Regeneration : MonoBehaviour, ICapacityItem
{
	public Health healthScript;
	public float efficiency = 1.0f;
	private string id;
	public float cost = 1.0f;
	int TO_REPLACE_money = 10000;

	public Regeneration(Health health, string name = "Regeneration")
	{
		healthScript = health;
		id = name;
	}

	public void DoEffect()
	{
		if (!healthScript) {
			Debug.Log("ERREUR RECUPERATION HEALTH SCRIPT");
		}
		healthScript.health = Mathf.Min(healthScript.health + efficiency * Time.deltaTime, healthScript.maxHealth);
		TO_REPLACE_money -= (int) (cost * Time.deltaTime);
	}

	public string GetId()
	{
		return id;
	}

}
