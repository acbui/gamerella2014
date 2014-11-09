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

	void Awake()
	{
		ins = this;
		DontDestroyOnLoad (this); 
	}

	// Use this for initialization
	void Start () {
		bossLevel = 1; 
	}
	
	// Update is called once per frame
	void Update () {
		updateGUI ();
		if (Input.GetKeyDown (KeyCode.Z))
		{
			bossUp ();
		}
	}

	void updateGUI()
	{
		healthbar.localScale = new Vector3 (((float) boss.HP)/ ((float) boss.maxHP), healthbar.localScale.y, healthbar.localScale.z);
	}

	void bossUp()
	{
		if (bossLevel < 3)
		{
			bossLevel++; 
			CamZoom cam = FindObjectOfType(typeof(CamZoom)) as CamZoom;
			cam.updateSize (bossLevel);
			Boss b = FindObjectOfType (typeof(Boss)) as Boss; 
			b.updateBoss (bossLevel); 
		}
	}
}
