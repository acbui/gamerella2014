using UnityEngine;
using System.Collections;

public class BossDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		Boss b = FindObjectOfType (typeof(Boss)) as Boss;
		b.getHit (col); 
	}
}
