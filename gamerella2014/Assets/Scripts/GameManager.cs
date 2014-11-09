using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject BossPrefab; 
	public Boss boss;

	public Transform healthbar; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		updateGUI ();
	}

	void updateGUI()
	{
		healthbar.localScale = new Vector3 (((float) boss.HP)/ ((float) boss.maxHP), healthbar.localScale.y, healthbar.localScale.z);
	}
}
