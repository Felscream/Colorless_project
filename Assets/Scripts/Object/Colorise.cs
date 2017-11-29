using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorise : MonoBehaviour {
	private MeshRenderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<MeshRenderer>();
	}

	// Update is called once per frame
	public void ColoriseTexture(float progressionValue)
	{
		rend.material.SetFloat("_Blend", Mathf.Max(rend.material.GetFloat("_Blend") - progressionValue, 0));

	}
	public void SetRatioColor(float ratio)
	{
		rend.material.SetFloat("_Blend", ratio);
	}
}
