﻿using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour 
{
	// boss stats
	public int HP;
	public int level; 

	// boss attacks 
	public GameObject swipe;
	public GameObject projectile;
	public GameObject laser; 
	public GameObject shield; 

	public int swipeCooldown;
	public int projectileCooldown;
	public int laserCooldown; 
	public int shieldCooldown;

	private int swipeCurrent;
	private int projectileCurrent;
	private int laserCurrent;
	private int shieldCurrent;

	// damage values
	public bool hit; 
	public int swordDamage;
	public int magicDamage; 
	public int arrowDamage; 

	void Start () 
	{
		HP = 500;
		level = 1; 

		swipeCurrent = swipeCooldown;
		projectileCurrent = projectileCooldown;
		laserCurrent = laserCooldown;
		shieldCurrent = shieldCooldown; 
	}

	void Update () 
	{
		coolDown ();
		attack ();
	}

	void coolDown()
	{
		swipeCurrent++;
		projectileCurrent++;
		laserCurrent++;
		shieldCurrent++; 
	}

	void attack()
	{
		// Grab. Available from the start. 
		if (Input.GetKeyDown (KeyCode.Alpha1))
		{
			if (projectileCurrent >= projectileCooldown)
			{
				Instantiate (projectile); 
				projectileCurrent = 0; 
			}
		}

		// Rock throw. 
		else if (Input.GetKeyDown (KeyCode.Alpha2))
		{
			if (level > 1)
			{
				if (swipeCurrent >= swipeCooldown)
				{
					Instantiate (swipe); 
					swipeCurrent = 0; 
				}
			}
		}

		// Laser. 
		else if (Input.GetKeyDown (KeyCode.Alpha3))
		{
			if (level > 2)
			{
				if (laserCurrent >= laserCooldown)
				{
					Instantiate (laser);
					laserCurrent = 0; 
				} 
			}
		}

		else if (Input.GetKeyDown (KeyCode.Space))
		{
			if (level > 3)
			{
				if (shieldCurrent >= shieldCooldown)
				{
					Instantiate (shield); 
					shieldCurrent = 0; 
				}
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (!hit)
		{
			if (col.tag == "Sword")
			{
				int damage = Random.Range (0, swordDamage+1);
				HP -= damage;
			}
			else if (col.tag == "Magic")
			{
				int damage = Random.Range (0, magicDamage+1);
				HP -= damage;
				Destroy (col.gameObject); 
			}

			else if (col.tag == "Arrow")
			{
				int damage = Random.Range (0, arrowDamage+1);
				HP -= damage; 
				Destroy (col.gameObject);
			}

			hit = true; 
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		hit = false; 
	}
}
