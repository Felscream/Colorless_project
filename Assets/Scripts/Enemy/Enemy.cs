using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float health;
    [SerializeField]
    private string prefabName;
    private string prefabFolder = "Prefabs/Enemy";
    private ColorChange colorChange;
    private float currentHealth;
    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
	// Use this for initialization
	private void Start () {
        colorChange = GetComponent<ColorChange>();
        currentHealth = health;
        if (colorChange == null)
        {
            Debug.Log("no color change");
        }

    }
	
	// Update is called once per frame
	private void Update () {
		
	}

    private void OnDestroy()
    {
        foreach (Transform child in gameObject.transform) { Destroy(child.gameObject); };
        Debug.Log(gameObject.transform.name + " killed");
    }


    public void ReceiveDamage(int damage, bool critical)
    {
        float tDamage = critical ? damage*1.5f : damage;

        currentHealth = currentHealth < 0 ? 0 : currentHealth - tDamage;
        
        Debug.Log("Enemy " + gameObject.transform.name + " damaged for "+ tDamage + " | health : "+ currentHealth);
        
        if(colorChange != null)
        {
            float greenPercentage = currentHealth / health;
            //Debug.Log(greenPercentage);
            colorChange.ChangeColor(greenPercentage);
        }
        CheckDeath();
    }
}
