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

	// to shift up with each level up 
	public Vector3[] shiftPositions;
	public int currentPos; 
	public float speed; 

	// turn on body parts on level up
	public GameObject level2torso;
	public GameObject level3belly; 

	// renderer materials
	public SpriteRenderer[] renderers;
	public Material[] initMaterials;

	void Start () 
	{
		level2torso = GameObject.Find ("boss_torso");
		level3belly = GameObject.Find ("boss_torso_lower");
		level2torso.SetActive (false);
		level3belly.SetActive (false); 
		level = 1;
		currentPos = level - 1; 

		swipeCurrent = swipeCooldown;
		projectileCurrent = projectileCooldown;
		laserCurrent = laserCooldown;
		shieldCurrent = shieldCooldown; 

		renderers = gameObject.GetComponentsInChildren<SpriteRenderer> () as SpriteRenderer[]; 
		initMaterials = new Material[renderers.Length]; 
		for (int i = 0; i < initMaterials.Length; i++)
		{
			initMaterials[i] = renderers[i].material; 
		}
		//_default = renderer.material;
	}

	void Update () 
	{
		coolDown ();
		attack ();
	}

	void FixedUpdate()
	{
		transform.position = Vector3.Lerp (transform.position, shiftPositions [currentPos], speed * Time.deltaTime); 
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

	public void getHit (Collider2D col)
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
			HP -= damage;
			Vector3 txtPos = new Vector3 (col.gameObject.transform.position.x + 22, col.gameObject.transform.position.y, col.gameObject.transform.position.z);
			Destroy (col.gameObject); 
			GameObject txt = Instantiate (dmgText, txtPos, Quaternion.identity) as GameObject; 
			if (damage > 0)
			{
				//GetComponent<SpriteRenderer>().material = _hit;
				foreach (SpriteRenderer r in renderers)
				{
					r.material = _hit;
				}
				txt.GetComponent<TextMesh>().text = "" + damage; 
			}
			else 
			{
				txt.GetComponent<TextMesh>().text = "Miss!"; 
			}
		}
		StartCoroutine (fadeHit ());

	}

	public void updateBoss(int lvl)
	{
		renderers = gameObject.GetComponentsInChildren<SpriteRenderer> () as SpriteRenderer[]; 
		initMaterials = new Material[renderers.Length]; 
		for (int i = 0; i < initMaterials.Length; i++)
		{
			initMaterials[i] = renderers[i].material; 
		}
		level = lvl;
		currentPos = lvl - 1;
		if (lvl == 2)
			level2torso.SetActive(true);
		if (lvl == 3)
			level3belly.SetActive(true);
	}

	IEnumerator fadeHit()
	{
		yield return new WaitForSeconds (fadeDelay); 
		//GetComponent<SpriteRenderer>().material = _default;
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].material = initMaterials[i]; 
		}
	}
}
