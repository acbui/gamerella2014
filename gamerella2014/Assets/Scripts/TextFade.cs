using UnityEngine;
using System.Collections;

public class TextFade : MonoBehaviour {

	private TextMesh mesh; 
	private bool fading; 
	public float delay;

	void Start () 
	{
		mesh = gameObject.GetComponent<TextMesh> ();
		fading = false; 
		StartCoroutine (delayFade ());
	}

	void FixedUpdate () 
	{
		if (fading)
		{
		}
	}
}
