using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour 
{
	public GameObject enemy;
	public float timer;
	public int cap;
	private int count;

	void Awake ()
	{
		timer = Time.time + 2;
	}
	// Use this for initialization
	void Start () 
	{
		count = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timer < Time.time && count < cap)
		{
			Spawn ();
			timer = Time.time + 2;
			count++;
		}
	}

	void Spawn ()
	{
		Instantiate (enemy, transform.position, Quaternion.identity);
		Debug.Log ("Birthed a hero of light");
	}
}
