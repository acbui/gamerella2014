using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour 
{
	public float aHP;
	public int aLevel; 
	public GameObject aTarget; 

	void Start () 
	{
		aHP = 100;
		aTarget = GameObject.Find ("MouseTarget"); 
		aLevel = 1; 
	}

	void Update () 
	{
		attack ();
	}

	void attack()
	{
		// Grab. Available from the start. 
		if (Input.GetKeyDown (KeyCode.Alpha1))
		{
			
		}

		// Rock throw. 
		else if (Input.GetKeyDown (KeyCode.Alpha2))
		{
			if (aLevel > 1)
			{
				
			}
		}

		// Laser. 
		else if (Input.GetKeyDown (KeyCode.Alpha3))
		{
			if (aLevel > 2)
			{
				
			}
		}

		else if (Input.GetKeyDown (KeyCode.Space))
		{
			if (aLevel > 3)
			{

			}
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{

	}
}
