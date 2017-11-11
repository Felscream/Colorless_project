using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character {
    [SerializeField]
    protected int attackDamage;
    [SerializeField]
    protected string prefabName;
    protected string prefabFolder = "Prefabs/Enemy";
    //private ColorChange colorChange;
	
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
