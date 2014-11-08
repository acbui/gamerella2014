using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	private static int numLasers;
	public int maxLasers;  
	public Vector3 laserTarget;
	public float speed; 
	public float delay; 

	public float xInc;
	public float yInc;

	// to extend the laser 
	public GameObject laser;
	
	// make the laser face the direction of the mouse at creation
	void Start () {
		laserTarget = GameObject.Find ("MouseTarget").transform.position; 

		
		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		diff.Normalize();
		
		float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot - 90);
	}

	void Update()
	{

	}

	void FixedUpdate()
	{
		transform.position = Vector3.Lerp (transform.position, laserTarget, Time.deltaTime * speed); 
		
		if (transform.position.y <= laserTarget.y + 0.05f)
		{
			StartCoroutine (delayDestroy());
		}
	}

	IEnumerator delayExtendLaser()
	{
		yield return new WaitForSeconds (delay); 
		if (numLasers < maxLasers)
			Instantiate (laser, new Vector3 (), transform.rotation); 
	}

	IEnumerator delayDestroy()
	{
		yield return new WaitForSeconds (delay);
		Destroy (gameObject); 
	}
}
