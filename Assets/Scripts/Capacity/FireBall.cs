using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ICapacityItem {

	string id;

	public FireBall (string name = "FireBall")
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
