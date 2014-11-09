using UnityEngine;
using System.Collections;

public class LaserExtend : MonoBehaviour {
	
	public float maxLaserScale;  
	public Vector3 laserTarget;
	public float speed; 
	public float delay; 

	public float angle; 

	// make the laser face the direction of the mouse at creation
	void Start () {
		laserTarget = GameObject.Find ("MouseTarget").transform.position; 
		
		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		diff.Normalize();
		
		angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
	}

	void FixedUpdate()
	{
		transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (transform.localScale.x, maxLaserScale, transform.localScale.z), Time.deltaTime * speed); 
		if (transform.localScale.y >= maxLaserScale - 0.05f)
		{
			Destroy (gameObject);  
		}
	}
}
