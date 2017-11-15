using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour {



	private const float L_HEALTH_BONUS = 0.25f; 
	private const float V0_VELOCITY_MULTIPLIER = 1.10f;
	private const float V1_VELOCITY_MULTIPLIER = 1.20f;
	private const float R1_COST_MULTIPLIER = 0.75f;
	private const float R2_COST_MULTIPLIER = 0.80f;
	private const float R2_REGEN_MULTIPLIER = 3.0f;
	private const float S0_JUMP_MULTIPLIER= 5.0f; 


	public bool l0 = false;
	public bool l1 = false;
	public bool l2 = false;
	public bool r0 = false;
	public bool r1 = false;
	public bool r2 = false;
	public bool v0 = false;
	public bool v1 = false;
	public bool s0 = false;
	public bool v2 = false;
	public bool e0 = false;
	public bool f0 = false;
	public bool g0 = false;



	public Health healthScript;
	public PlayerMovement playerMovement;
	public Inventory inventory;

	public bool L0
	{
		get { return l0; }
		set
		{
			if (value)//&& !l0)
			{
				healthScript.maxHealth += healthScript.baseHealth * L_HEALTH_BONUS;
			}
			else
			{
				healthScript.maxHealth -= healthScript.baseHealth * L_HEALTH_BONUS;
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
			if (value)//&& !l1)
			{
				healthScript.maxHealth += healthScript.baseHealth * L_HEALTH_BONUS;
			}
			else
			{
				healthScript.maxHealth -= healthScript.baseHealth * L_HEALTH_BONUS;
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
			if (value)//&& !l2)
			{
				healthScript.maxHealth += healthScript.baseHealth * L_HEALTH_BONUS;
			}
			else
			{
				healthScript.maxHealth -= healthScript.baseHealth * L_HEALTH_BONUS;
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
			if (value)//&& !r0)
			{
				inventory.AddCapacity(new Regeneration(healthScript, "Regeneration"));
			}
			else
			{
				inventory.DeleteCapacity("Regeneration");
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
			if (value )//&&!r1)
			{
				((Regeneration)inventory.GetCapacity("Regeneration")).cost *= R1_COST_MULTIPLIER;
			}
			else
			{
				((Regeneration)inventory.GetCapacity("Regeneration")).cost *= 2 - R1_COST_MULTIPLIER;

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
			if (value)//&& !r2)
			{
				((Regeneration)inventory.GetCapacity("Regeneration")).cost *= R2_COST_MULTIPLIER;
				((Regeneration)inventory.GetCapacity("Regeneration")).efficiency *= R2_REGEN_MULTIPLIER;

			}
			else
			{
				((Regeneration)inventory.GetCapacity("Regeneration")).cost *= 2 - R2_COST_MULTIPLIER;
				((Regeneration)inventory.GetCapacity("Regeneration")).efficiency *= 2 - R2_REGEN_MULTIPLIER;

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
			if (value)//&& !v0)
			{

				playerMovement.speed *= V0_VELOCITY_MULTIPLIER;
			}
			else
			{
				playerMovement.speed *= 2 - V0_VELOCITY_MULTIPLIER;
			}
			v0 = value;
		}
	}

	public bool V1
	{
		get
		{
			return v1;
		}

		set
		{
			if (value)//&& !v1)
			{

				playerMovement.speed *= V1_VELOCITY_MULTIPLIER;
			}
			else
			{
				playerMovement.speed *= 2 - V1_VELOCITY_MULTIPLIER;
			}
			v1 = value;
		}
	}

	public bool S0
	{
		get
		{
			return s0;
		}

		set
		{
			if (value)//&& !s0)
			{
				playerMovement.jumpVelocity *= S0_JUMP_MULTIPLIER;
			}
			else
			{
			playerMovement.jumpVelocity *= S0_JUMP_MULTIPLIER;
			}
			s0 = value;
		}
	}

	public bool V2
	{
		get
		{
			return v2;
		}

		set
		{
			if (value)//&& !v2)
			{
				inventory.AddCapacity(new Blink("Blink"));
			}
			else
			{
				inventory.DeleteCapacity("Blink");
			}
			v2 = value;
		}
	}

	public bool E0
	{
		get
		{
			return e0;
		}

		set
		{
			if (value)//&& !e0)
			{
				inventory.AddCapacity(new EnergyBall("EnergyBall"));
			}
			else
			{
				inventory.DeleteCapacity("EnergyBall");
			}
			e0 = value;
		}
	}

	public bool F0
	{
		get
		{
			return f0;
		}

		set
		{
			if (value)//&& !f0)
			{
				inventory.AddCapacity(new FireBall("FireBall"));
			}
			else
			{
				inventory.DeleteCapacity("FireBall");
			}
			f0 = value;
		}
	}

	public bool G0
	{
		get
		{
			return g0;
		}

		set
		{
			if (value)//&& !g0)
			{
				inventory.AddCapacity(new Freeze("Freeze"));
			}
			else
			{
				inventory.DeleteCapacity("Freeze");
			}
			g0 = value;
		}
	}

	public bool M0 = false;
	public bool M1 = false;
	public bool B0 = false;
	public bool B1 = false;





	// Use this for initialization
	void Start () {
		healthScript = GetComponent<Health>();
		playerMovement = GetComponent<PlayerMovement>();
		inventory = GetComponent<Inventory>();


		//Utile pour debug : force l'apprentissage des competences au lancement;
		if (L0) L0 = true;
		if (L1) L1 = true;
		if (L2) L2 = true;
		if (R0) R0 = true;
		if (R1) R1 = true;
		if (R2) R2 = true;
		if (V0) V0 = true;
		if (V1) V1 = true;
		if (S0) S0 = true;
		if (S0) S0 = true;
		if (S0) S0 = true;
		if (V2) V2 = true;
		if (E0) E0 = true;
		if (F0) F0 = true;
		if (G0) G0 = true;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
