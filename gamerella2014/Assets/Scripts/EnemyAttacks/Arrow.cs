using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public CircleCollider2D[] targets; 
	public Vector3 arrowTarget;
	public float speed;

	void Start () {
		targets = FindObjectsOfType(typeof(CircleCollider2D)) as CircleCollider2D[];
		if (GameManager.ins.bossLevel == 1)
		{
			arrowTarget = GameObject.Find ("level1_head").transform.position; 
		}
		else 
		{
			bool setTarget = false; 
			while (!setTarget)
			{
				int index = Random.Range (0, targets.Length);
				if (targets[index].enabled)
				{
					CircleCollider2D col = targets[index];
					arrowTarget = col.gameObject.transform.position; 
				}
				setTarget = true;
			}
			/*if (GameManager.ins.bossLevel >= 2)
			{ 
				while (arrowTarget == null)
				{
					CircleCollider2D col = targets[Random.Range (0, targets.Length)];
					if (col.gameObject.activeSelf && (col.gameObject.name.Contains ("level2_") || col.gameObject.name.Contains ("level1_")))
					{
						arrowTarget = col.gameObject.transform.position; 
					}
				}
			}
			else
			{
				targets = FindObjectsOfType(typeof(CircleCollider2D)) as CircleCollider2D[]; 
				while (arrowTarget == null)
				{
					CircleCollider2D col = targets[Random.Range (0, targets.Length)];
					if (col.gameObject.activeSelf)
					{
						arrowTarget = col.gameObject.transform.position; 
					}
				}
			}*/
		}

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
