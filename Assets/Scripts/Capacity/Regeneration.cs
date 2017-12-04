using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Regeneration : ICapacityItem
{
	public Player player;
	public float efficiency = 10.0f;
	private string id;
	public float cost = 250.0f;

	public Regeneration(Player player, string name = "Regeneration")
	{
		this.player = player;
		id = name;
	}

	public void DoEffect()
	{
        int lifeGem = player.GetComponent<Inventory>().GetLifeGem();
		if (!player) {
			Debug.Log("ERREUR RECUPERATION HEALTH SCRIPT");
		}
		Debug.Log("REGEN");
        if(lifeGem > 0 && player.GetCurrentHealth() < player.MaxHealth)
        {
            player.Heal(efficiency * Time.deltaTime);
            lifeGem -= (int)(cost * Time.deltaTime);
            if(lifeGem < 0){
                lifeGem = 0;
            }
        }
        Debug.Log(lifeGem);
        player.GetComponent<Inventory>().SetLifeGem(lifeGem);

    }

	public string GetId()
	{
		return id;
	}

}
