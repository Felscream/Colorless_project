using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoriseRoom : MonoBehaviour {

	public Colorise[] colorElements;
	public float TestRatio = 0.1f;
	void Start()
	{
		colorElements = GetComponentsInChildren<Colorise>();

		foreach (Colorise element in colorElements)
			element.SetRatioColor(1);
	}

	// Update is called once per frame
	void ColoriseRoomTexture (float progressionValue) {
		foreach(Colorise element in colorElements)
			element.ColoriseTexture(progressionValue);
	}

	void Update()
	{
		ColoriseRoomTexture(TestRatio);
	}
}
