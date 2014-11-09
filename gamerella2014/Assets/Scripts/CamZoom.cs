using UnityEngine;
using System.Collections;

public class CamZoom : MonoBehaviour {
	
	public float[] orthoSizes; 
	public int currentSize; 
	public float speed; 
	
	void Start () 
	{
		currentSize = 0;
	}

	void FixedUpdate () 
	{
		gameObject.camera.orthographicSize = Mathf.Lerp (gameObject.camera.orthographicSize, orthoSizes[currentSize], Time.deltaTime * speed);
	}

	public void updateSize(int lvl)
	{
		currentSize = lvl - 1; 
	}
}
