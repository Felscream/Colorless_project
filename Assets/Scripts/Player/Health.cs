using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {


	public float baseHealth = 100.0f;
	public float maxHealth = 100.0f;
	public float health = 100.0f;
	public float regeneration = 0.0f;

	void Update() {
		health = Mathf.Min(maxHealth, health + regeneration * Time.deltaTime);
	}

}
