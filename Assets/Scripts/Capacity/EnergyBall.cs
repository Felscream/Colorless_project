using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : ICapacityItem
{

	string id;

	public EnergyBall(string name = "EnergyBall")
	{
		id = name;
	}

	public void DoEffect()
	{

	}

	public string GetId()
	{
		return id;
	}
}
