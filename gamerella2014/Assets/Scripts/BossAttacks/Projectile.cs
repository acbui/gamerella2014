using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Vector3 throwTarget;
	public float speed; 
	public float delay; 

	// make the projectile face the direction of the mouse at creation
	void Start () {
		throwTarget = GameObject.Find ("MouseTarget").transform.position; 

		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		diff.Normalize();
		
		float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot - 90);
	}


	void FixedUpdate()
	{
		transform.position = Vector3.Lerp (transform.position, throwTarget, Time.deltaTime * speed); 

		if (transform.position.y <= throwTarget.y + 0.05f)
		{
			StartCoroutine (delayDestroy());
		}
	}

	IEnumerator delayDestroy()
	{
		// play animation
		yield return new WaitForSeconds (delay);
		Destroy (gameObject); 
	}
}
