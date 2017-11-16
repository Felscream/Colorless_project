using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character {
    private static Player instance;
    private Inventory inventory;
    private Text playerHealthUI;
	[SerializeField]


	protected override IEnumerator Die()
    {
        yield return null;
        GameObject.FindGameObjectWithTag("Gameover").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("AmmoDisplay").GetComponent<Text>().enabled = false;
    }

    private new void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            dead = false;
			currentHealth = base.BaseHealth;
			MaxHealth = base.BaseHealth;
		}
        playerHealthUI = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
        
    }

    private void Start()
    {
        UpdateHealthUI();
        inventory = Inventory.GetInstance();
    }

    private void UpdateHealthUI()
    {
        playerHealthUI.text = ((int)currentHealth).ToString();
        CheckDeath();
    }

    private void LateUpdate()
    {
        UpdateHealthUI();
    }

    public static Player GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("No instance of " + typeof(Player));
            return null;
        }
        return instance;
    }

    public void ReceiveDamage(int damage)
    {
        int tDamage = damage;
        currentHealth -= tDamage;
        currentHealth = currentHealth < 0 ? 0 : currentHealth;
        UpdateHealthUI();
    }
    public void OnCollisionEnter(Collision col)
    {
        Item item = col.gameObject.GetComponent<Item>();
        if (item != null && inventory != null)
        {
            switch (item.GetType().ToString())
            {
                case "LifeGemItem":
                    LifeGemItem lifeGem = (LifeGemItem)item;
                    inventory.CollectLifeGem(lifeGem.GetAmount());
                    item.DestroyItem();
                    break;
            }
        }
    }


	public void SetHealth(float value)
	{
		currentHealth = Mathf.Min(MaxHealth, value);
	}    

	public void Heal(float amount)
	{
		currentHealth = Mathf.Min(MaxHealth, currentHealth + amount);
	}
}
