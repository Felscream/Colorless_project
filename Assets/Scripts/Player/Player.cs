using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character {
    private static Player instance;
    private Text playerHealthUI;

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
            currentHealth = health;
        }
        playerHealthUI = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
        
    }

    private void Start()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        playerHealthUI.text = currentHealth.ToString();
        CheckDeath();
    }

    /*private void LateUpdate()
    {
        UpdateHealthUI();
    }*/

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
        int tDamage = (int)Mathf.Ceil(damage);

        currentHealth = currentHealth < 0 ? 0 : currentHealth - tDamage;

        Debug.Log("Player damaged for " + tDamage + " | health : " + currentHealth);
        UpdateHealthUI();
    }
}
