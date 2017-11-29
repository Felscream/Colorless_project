using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoriseRoom : MonoBehaviour {

	public Colorise[] colorElements;
	public float TestRatio = 0.1f;
	private int totalEnemyQuantity = 0;
	private bool init = false;
	void FixedUpdate()
	{
		if (!init)
		{
			colorElements = GetComponentsInChildren<Colorise>();

			foreach (Colorise element in colorElements)
			{
				element.SetRatioColor(1.0f);
			}

			init = true;
		}
	}

	// Update is called once per frame
	public void ColoriseRoomTexture (float progressionValue) {
		foreach (Colorise element in colorElements)
		{
			Debug.Log("Corolorise Element: " + progressionValue);
			element.ColoriseTexture(progressionValue);
		}

	}

	void Update()
	{
		//ColoriseRoomTexture(TestRatio * Time.deltaTime);
	}

	public void IncreaseEnemyQuantity(int quantity) {
		totalEnemyQuantity += quantity;
	}

	public int GetEnemyQuantity()
	{
		return totalEnemyQuantity;
	}
}
