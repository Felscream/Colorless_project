using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour, ICapacityItem
{
	public Health healthScript;
	public float efficiency = 1.0f;

	public void DoEffect()
	{
		Debug.Log("TEEEEST2");
		if (!healthScript) {
			healthScript = GetComponentInParent<Health>();
			Debug.Log("TEEEEST");
		}
		healthScript.health += efficiency * Time.deltaTime;

	}

}
