using UnityEngine;
using System.Collections;

public class RotateToMouse : MonoBehaviour 
{
	
	void Start () 
	{
	
	}

	void Update () 
	{
		if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y <= 5)
		{
			Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			diff.Normalize();
			
			float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot-90);
		}
	}
}
