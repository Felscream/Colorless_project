using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour {



	private const float HEALTH_MULTIPLIER = 1.25f; 
	private const float REGEN_INCREASE = 1.0f; 
	private const float VELOCITY_MULTIPLIER = 1.25f;

	private bool l0 = false;
	private bool l1 = false;
	private bool l2 = false;
	private bool r0 = false;
	private bool r1 = false;
	private bool r2 = false;
	private bool v0 = false;


	public Health healthScript;
	public PlayerMovement playerMovement;

	public bool L0
	{
		get { return l0; }
		set
		{
			if (value && !l0)
			{
				healthScript.baseHealth *= HEALTH_MULTIPLIER;
			}
			else
			{
				healthScript.baseHealth *= 2 - HEALTH_MULTIPLIER;
			}
			l0 = value;
		}
	}

	public bool L1
	{
		get
		{
			return l1;
		}

		set
		{
			if (value && !l1)
			{
				healthScript.baseHealth *= HEALTH_MULTIPLIER;
			}
			else
			{
				healthScript.baseHealth *= 2 - HEALTH_MULTIPLIER;
			}
			l1 = value;
		}
	}

	public bool L2
	{
		get
		{
			return l2;
		}

		set
		{
			if (value && !l2)
			{
				healthScript.baseHealth *= HEALTH_MULTIPLIER;
			}
			else
			{
				healthScript.baseHealth *= 2 - HEALTH_MULTIPLIER;
			}
			l2 = value;
		}
	}

	public bool R0
	{
		get
		{
			return r0;
		}

		set
		{
			if (value && !r0)
			{
				healthScript.regeneration += REGEN_INCREASE;
			}
			else
			{
				healthScript.regeneration += REGEN_INCREASE;
			}
			r0 = value;
		}
	}

	public bool R1
	{
		get
		{
			return r1;
		}

		set
		{
			if (value && !r1)
			{
				healthScript.regeneration += REGEN_INCREASE;
			}
			else
			{
				healthScript.regeneration += REGEN_INCREASE;
			}
			r1 = value;
		}
	}

	public bool R2
	{
		get
		{
			return r2;
		}

		set
		{
			if (value && !r2)
			{
				healthScript.regeneration += REGEN_INCREASE;
			}
			else
			{
				healthScript.regeneration += REGEN_INCREASE;
			}
			r2 = value;
		}
	}

	public bool V0
	{
		get
		{
			return v0;
		}

		set
		{
			if (value && !v0)
			{

				playerMovement.speed *= VELOCITY_MULTIPLIER;
			}
			else
			{
				playerMovement.speed *= 2 - VELOCITY_MULTIPLIER;
			}
			v0 = value;
		}
	}

	public bool V1 = false;
	public bool V2 = false;
	public bool S0 = false;
	public bool E0 = false;
	public bool F0 = false;
	public bool G0 = false;
	public bool M0 = false;
	public bool M1 = false;
	public bool B0 = false;
	public bool B1 = false;





	// Use this for initialization
	void Start () {
		healthScript = GetComponent<Health>();
		playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
