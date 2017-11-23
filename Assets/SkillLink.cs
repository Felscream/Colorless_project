using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillLink : MonoBehaviour {

	[SerializeField]
	private SkillButton origin, destination;




	public void MakeAvailable()
	{
		destination.MakeAvailable();
		GetComponent<Image>().color = Color.green;

	}

}
