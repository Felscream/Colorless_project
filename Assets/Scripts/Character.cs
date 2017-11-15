using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	[SerializeField]
	private int baseHealth, maxHealth;
	[SerializeField]
	protected float currentHealth;
    protected bool dead;

	public int BaseHealth
	{
		get
		{
			return baseHealth;
		}
	}

	public int MaxHealth
	{
		get
		{
			return maxHealth;
		}

		set
		{
			maxHealth = value;
		}
	}

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
        currentHealth = BaseHealth;
        MaxHealth = BaseHealth;
	}

    protected abstract IEnumerator Die();

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsDead()
    {
        return dead;
    }
}
