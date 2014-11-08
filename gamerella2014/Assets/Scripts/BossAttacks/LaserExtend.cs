using UnityEngine;
using System.Collections;

public class LaserExtend : MonoBehaviour {
	
	public float maxLaserScale;  
	public Vector3 laserTarget;
	public float speed; 
	public float delay; 

	public laserRetract retractScript; 
	public GameObject retract;

	// make the laser face the direction of the mouse at creation
	void Start () {
		laserTarget = GameObject.Find ("MouseTarget").transform.position; 

		retractScript = gameObject.GetComponentInParent<laserRetract> ();
		retract = retractScript.gameObject; 
		
		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - retract.transform.position;
		diff.Normalize();
		
		float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		retract.transform.rotation = Quaternion.Euler(0f, 0f, rot - 90);
	}

	void FixedUpdate()
	{
		transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (transform.localScale.x, maxLaserScale, transform.localScale.z), Time.deltaTime * speed); 
		if (transform.position.y >= maxLaserScale - 0.05f)
		{
			StartCoroutine (delayRetract());
		}
	}

	IEnumerator delayRetract()
	{
		yield return new WaitForSeconds (delay);
		retractScript.enabled = true; 
	}
}
