using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Regeneration : ICapacityItem
{
	public Player player;
	public float efficiency = 10.0f;
	private string id;
	public float cost = 1.0f;
	int TO_REPLACE_money = 10000;

	public Regeneration(Player player, string name = "Regeneration")
	{
		this.player = player;
		id = name;
	}

	public void DoEffect()
	{
		if (!player) {
			Debug.Log("ERREUR RECUPERATION HEALTH SCRIPT");
		}
		Debug.Log("REGEN");

		player.Heal(efficiency * Time.deltaTime);
		TO_REPLACE_money -= (int) (cost * Time.deltaTime);
	}

	public string GetId()
	{
		return id;
	}

}
