using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    protected int health;

    protected int currentHealth;
    protected bool dead;

    protected void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            dead = true;
            StartCoroutine(Die());
        }
    }
    // Use this for initialization
    protected void Awake () {
        dead = false;
        currentHealth = health;
	}

    protected abstract IEnumerator Die();

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsDead()
    {
        return dead;
    }
}
