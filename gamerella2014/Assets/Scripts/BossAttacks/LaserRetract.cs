using UnityEngine;
using System.Collections;

public class laserRetract : MonoBehaviour {
	
	public float speed; 
	public float delay; 
	
	// make the laser face the direction of the mouse at creation
	void Start () {
	}
	
	void FixedUpdate()
	{
		transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (transform.localScale.x, 0, transform.localScale.z), Time.deltaTime * speed); 
		if (transform.position.y <= 0.05f)
		{
			StartCoroutine (delayDestroy());
		}
	}
	
	IEnumerator delayDestroy()
	{
		yield return new WaitForSeconds (delay);
		Destroy (gameObject);
	}
}
