using UnityEngine;
using System.Collections;

public class TextFade : MonoBehaviour {

	private TextMesh mesh; 
	private bool fading; 
	public float delay;
	public float speed; 

	void Start () 
	{
		mesh = gameObject.GetComponent<TextMesh> ();
		fading = false; 
		StartCoroutine (delayFade ());
	}

	void FixedUpdate () 
	{
		if (fading)
			mesh.color = Color.Lerp (mesh.color, new Color (mesh.color.r, mesh.color.g, mesh.color.b, 0), speed*Time.deltaTime);
		if (mesh.color.a <= 0.05f)
			Destroy (gameObject); 
	}

	IEnumerator delayFade()
	{
		yield return new WaitForSeconds (delay);
		fading = true; 
	}
}
