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

	public int swipeCurrent;
	public int projectileCurrent;
	public int laserCurrent;
	public int shieldCurrent;

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

	// animation stuff
	public Animator anim; 

	public SpriteRenderer[] torsors;
	public SpriteRenderer[] bellyrs;

	public Collider2D[] torsocols;
	public Collider2D[] bellycols;

	public AudioClip[] clips;

	void Start () 
	{
		level = 1;
		currentPos = level - 1; 

		anim = gameObject.GetComponent<Animator> (); 
		level2torso = GameObject.Find ("boss_torso");
		level3belly = GameObject.Find ("boss_torso_lower");

		torsors = level2torso.GetComponentsInChildren<SpriteRenderer>() as SpriteRenderer[];
		bellyrs = level3belly.GetComponentsInChildren<SpriteRenderer>() as SpriteRenderer[];
		
		torsocols = level2torso.GetComponentsInChildren<Collider2D>() as Collider2D[];
		bellycols = level3belly.GetComponentsInChildren<Collider2D>() as Collider2D[];

		updateParts ();

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
				audio.PlayOneShot(clips[3]);
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
					audio.PlayOneShot (clips[4]);
					if (GameObject.Find ("MouseTarget").transform.position.x > 0)
					{
						anim.SetInteger ("BossAttack", 2);
						StartCoroutine (stopAnimation(0.75f));
					}
					else 
					{
						anim.SetInteger ("BossAttack", 1); 
						StartCoroutine (stopAnimation(0.75f));
					}
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
					if (GameObject.Find ("MouseTarget").transform.position.x > 0)
					{
						anim.SetInteger ("BossAttack", 4);
						StartCoroutine (shootLaser(0.3f)); 
						StartCoroutine (stopAnimation(0.75f));
					}
					else 
					{
						anim.SetInteger ("BossAttack", 3); 
						StartCoroutine (shootLaser(0.30f));
						StartCoroutine (stopAnimation(0.75f));
					}
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
			audio.PlayOneShot(clips[0]);
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
		updateParts ();
		if (renderers [0].material == _hit) 
		{
			for (int i = 0; i < renderers.Length; i++)
			{
				renderers[i].material = initMaterials[i]; 
			}
		}
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

	IEnumerator stopAnimation(float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		anim.SetInteger ("BossAttack", 0); 
	}

	IEnumerator shootLaser (float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		int cl = Random.Range (0, 2);
		audio.PlayOneShot (clips [cl]);
		Vector3 laserPos = GameObject.Find ("LaserEnd").transform.position;
		Instantiate (laser, laserPos, Quaternion.identity);
	}

	public void updateParts()
	{
		if (level == 1)
		{
			foreach (SpriteRenderer r in torsors)
			{
				r.enabled = false; 
			}
			foreach (SpriteRenderer r in bellyrs)
			{
				r.enabled = false; 
			}
			foreach (Collider2D c in torsocols)
			{
				c.enabled = false; 
			}
			foreach (Collider2D c in bellycols)
			{
				c.enabled = false; 
			}
		}
		else if (level == 2)
		{
			foreach (SpriteRenderer r in torsors)
			{
				r.enabled = true; 
			}
			foreach (SpriteRenderer r in bellyrs)
			{
				r.enabled = false; 
			}
			foreach (Collider2D c in torsocols)
			{
				c.enabled = true; 
			}
			foreach (Collider2D c in bellycols)
			{
				c.enabled = false; 
			}
		}
		else 
		{
			foreach (SpriteRenderer r in torsors)
			{
				r.enabled = true; 
			}
			foreach (SpriteRenderer r in bellyrs)
			{
				r.enabled = true; 
			}
			foreach (Collider2D c in torsocols)
			{
				c.enabled = true; 
			}
			foreach (Collider2D c in bellycols)
			{
				c.enabled = true; 
			}
		}
	}
}
