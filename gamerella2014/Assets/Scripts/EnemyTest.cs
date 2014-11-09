using UnityEngine;
using System.Collections;

public class EnemyTest : MonoBehaviour {

	public GameObject arrow; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A))
		{
			Instantiate (arrow, transform.position, Quaternion.identity);
		}
	}
}
