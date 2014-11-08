using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public float laserWidth = 1.0f;
	public float noise = 1.0f;
	public float maxLength = 50.0f;
	public Color color = Color.red;
	
	
	LineRenderer lineRenderer;
	int length;
	Vector3[] position;
	Vector3 offset;
	
	
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetWidth(laserWidth, laserWidth);
		offset = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		RenderLaser();
	}
	
	void RenderLaser(){
		
		//Shoot our laserbeam forwards!
		UpdateLength();
		
		lineRenderer.SetColors(color,color);
		//Move through the Array
		for(int i = 0; i<length; i++){
			//Set the position here to the current location and project it in the forward direction of the object it is attached to
			offset.x =transform.position.x+i*transform.forward.x+Random.Range(-noise,noise);
			offset.z =i*transform.forward.z+Random.Range(-noise,noise)+transform.position.z;
			position[i] = offset;
			position[0] = transform.position;
			
			lineRenderer.SetPosition(i, position[i]);
			
		}
		
		
		
	}
	
	void UpdateLength(){
		//Raycast from the location of the cube forwards
		RaycastHit[] hit;
		hit = Physics.RaycastAll(transform.position, Vector3.forward, maxLength);
		int i = 0;
		while(i < hit.Length){
			//Check to make sure we aren't hitting triggers but colliders
			if(!hit[i].collider.isTrigger)
			{
				length = (int)Mathf.Round(hit[i].distance)+2;
				position = new Vector3[length];
				lineRenderer.SetVertexCount(length);
				return;
			}
			i++;
		}
		length = (int)maxLength;
		position = new Vector3[length];
		lineRenderer.SetVertexCount(length);
		
		
	}
}
