using UnityEngine;
using System.Collections;

public class MouseTarget : MonoBehaviour 
{
	
	void Start () 
	{
	
	}

	void Update () 
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;
		gameObject.transform.position = mousePos;
	}
}
