using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager ins; 

	public int bossLevel; 
	public int enemyCount; 

	// for initializing the game
	public GameObject BossPrefab; 
	public Boss boss;

	// for updating the healthbar
	public Transform healthbar; 

	public GameObject bossUpText;
	public SpawnScript[] l1spawns;
	public SpawnScript[] l2spawns;
	public SpawnScript[] l3spawns;

	void Awake()
	{
		ins = this;
		DontDestroyOnLoad (this); 
	}

	// Use this for initialization
	void Start () {
		bossLevel = 1; 
		GameObject txt = Instantiate (bossUpText) as GameObject;
		txt.GetComponent<TextMesh>().text = "Level " + bossLevel;
	}
	
	// Update is called once per frame
	void Update () {
		updateGUI ();
		if (Input.GetKeyDown (KeyCode.Z))
		{
			//bossUp ();
		}

		if (bossLevel == 1 && enemyCount >= 3)
		{
			foreach (SpawnScript s in l1spawns)
			{
				Destroy (s.gameObject);
			}
			foreach (SpawnScript s in l2spawns)
			{
				s.enabled = true;
			}
			bossUp ();
		}
		else if (bossLevel == 2 && enemyCount >= 6)
		{
			foreach (SpawnScript s in l2spawns)
			{
				Destroy (s.gameObject);
			}
			foreach (SpawnScript s in l3spawns)
			{
				s.enabled = true;
			}
			bossUp();
		}
		else if (bossLevel == 3 && enemyCount >= 12)
		{
			Application.LoadLevel ("Win");
		}
		checkHealth ();
	}

	void updateGUI()
	{
		healthbar.localScale = new Vector3 (((float) boss.HP)/ ((float) boss.maxHP), healthbar.localScale.y, healthbar.localScale.z);
	}

	public void bossUp()
	{
		enemyCount = 0;
		if (bossLevel < 3)
		{
			bossLevel++; 
			GameObject txt = Instantiate (bossUpText) as GameObject;
			txt.GetComponent<TextMesh>().text = "Level " + bossLevel;
			CamZoom cam = FindObjectOfType(typeof(CamZoom)) as CamZoom;
			cam.updateSize (bossLevel);
			Boss b = FindObjectOfType (typeof(Boss)) as Boss; 
			b.updateBoss (bossLevel); 
		}
	}

	void checkHealth()
	{
		if (boss.HP <= 0)
		{
			//Application.LoadLevel ("Lose");
		}
	}

	void initialize()
	{
		GameObject cam = GameObject.Find ("Main Camera");
		CamZoom c = cam.GetComponent<CamZoom> ();
		c.currentSize = 0;
	}

	void initializeBoss()
	{
		GameObject b = Instantiate (BossPrefab) as GameObject;
		boss = b.GetComponent<Boss> ();

		boss.level = 1;
		boss.currentPos = boss.level - 1; 
		
		boss.anim = boss.GetComponent<Animator> (); 
		boss.level2torso = GameObject.Find ("boss_torso");
		boss.level3belly = GameObject.Find ("boss_torso_lower");
		
		boss.torsors = boss.level2torso.GetComponentsInChildren<SpriteRenderer>() as SpriteRenderer[];
		boss.bellyrs = boss.level3belly.GetComponentsInChildren<SpriteRenderer>() as SpriteRenderer[];
		
		boss.torsocols = boss.level2torso.GetComponentsInChildren<Collider2D>() as Collider2D[];
		boss.bellycols = boss.level3belly.GetComponentsInChildren<Collider2D>() as Collider2D[];
		
		boss.updateParts ();
		
		boss.swipeCurrent = boss.swipeCooldown;
		boss.projectileCurrent = boss.projectileCooldown;
		boss.laserCurrent = boss.laserCooldown;
		boss.shieldCurrent = boss.shieldCooldown; 
		
		boss.renderers = gameObject.GetComponentsInChildren<SpriteRenderer> () as SpriteRenderer[]; 
		boss.initMaterials = new Material[boss.renderers.Length]; 
		for (int i = 0; i < boss.initMaterials.Length; i++)
		{
			boss.initMaterials[i] = boss.renderers[i].material; 
		}
	}
}
