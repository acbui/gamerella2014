using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public Vector3 arrowTarget;
	public float speed;

	void Start () {
		arrowTarget = GameObject.Find ("Boss").transform.position; 

		// point the arrow towards the boss
		Vector3 diff = arrowTarget - transform.position;
		diff.Normalize();
		
		float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot - 90);
	}

	void FixedUpdate()
	{
		transform.position = Vector3.Lerp (transform.position, arrowTarget, Time.deltaTime * speed); 
	}
}
