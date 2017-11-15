using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : ICapacityItem {

	string id;

	public Freeze(string name = "Freeze")
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
