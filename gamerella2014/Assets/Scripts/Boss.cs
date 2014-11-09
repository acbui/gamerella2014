using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour 
{
	// boss stats
	public int HP;
	public int level; 

	// set stats
	public int maxHP;

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
	public int swordDamage;
	public int magicDamage; 
	public int arrowDamage; 

	public GameObject dmgText;

	public Material _default;
	public Material _hit;
	public float fadeDelay; 

	void Start () 
	{
		level = 5; 

		swipeCurrent = swipeCooldown;
		projectileCurrent = projectileCooldown;
		laserCurrent = laserCooldown;
		shieldCurrent = shieldCooldown; 

		_default = renderer.material;
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
		// Throw
		if (Input.GetKeyDown (KeyCode.Alpha1))
		{
			if (projectileCurrent >= projectileCooldown)
			{
				Instantiate (projectile); 
				projectileCurrent = 0; 
			}
		}

		// Swipe
		else if (Input.GetKeyDown (KeyCode.Alpha2))
		{
			if (level > 1)
			{
				if (swipeCurrent >= swipeCooldown)
				{
					if (GameObject.Find ("MouseTarget").transform.position.x > 0)
					{
						Instantiate (swipe); 
					}
					else 
					{
						Instantiate (swipe); 
					}
					swipeCurrent = 0; 
				}
			}
		}

		// Laser. 
		else if (Input.GetKeyDown (KeyCode.Alpha3))
		{
			print ("key");
			if (level > 2)
			{
				if (laserCurrent >= laserCooldown)
				{
					print ("lasers");
					//Instantiate (laser);
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
		int damage = 0; 
	
		if (col.tag == "Sword")
		{
			damage = Random.Range (0, swordDamage+1);
		}
		else if (col.tag == "Magic")
		{
			damage = Random.Range (0, magicDamage+1);
		}

		else if (col.tag == "Arrow")
		{
			damage = Random.Range (0, arrowDamage+1); 
		}

		if (col.tag == "Sword" || col.tag == "Magic" || col.tag == "Arrow" )
		{
			Destroy (col.gameObject); 
			HP -= damage;
			Vector3 txtPos = new Vector3 (col.gameObject.transform.position.x + 22, col.gameObject.transform.position.y, col.gameObject.transform.position.z);
			GameObject txt = Instantiate (dmgText, txtPos, Quaternion.identity) as GameObject; 
			if (damage > 0)
			{
				GetComponent<SpriteRenderer>().material = _hit;
				txt.GetComponent<TextMesh>().text = "" + damage; 
			}
			else 
			{
				txt.GetComponent<TextMesh>().text = "Miss!"; 
			}
		}
		StartCoroutine (fadeHit ());

	}

	IEnumerator fadeHit()
	{
		yield return new WaitForSeconds (fadeDelay); 
		GetComponent<SpriteRenderer>().material = _default;
	}
}
