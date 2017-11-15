using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : ICapacityItem
{
	string id;

	public Blink(string name = "Blink")
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
