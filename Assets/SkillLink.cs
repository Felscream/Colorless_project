using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillLink : MonoBehaviour {

	[SerializeField]
	public SkillButton origin, destination;




	public void MakeAvailable()
	{
		destination.MakeAvailable();
		Color linkColor = GetComponent<Image>().color;
		linkColor.r = 0.01f;
		linkColor.g = 0.6f;
		linkColor.b = 0.01f;
		GetComponent<Image>().color = linkColor;
	}

}
