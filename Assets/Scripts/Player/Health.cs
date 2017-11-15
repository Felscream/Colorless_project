using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {


	public float baseHealth = 100.0f;
	public float maxHealth = 100.0f;
	public float health = 100.0f;

	void TakeDamage(int amount)
	{
		health -= amount;
	}

	void Heal(int amount)
	{
		health += amount;
	}
}
