using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public CircleCollider2D[] targets; 
	public Vector3 arrowTarget;
	public float speed;
	public CircleCollider2D coll;

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
					coll = col;
					arrowTarget = col.gameObject.transform.position; 
					if (GameManager.ins.bossLevel == 2 || coll == null)
					{
						if (col.gameObject.name.Contains ("level3") || coll == null)
						{
							arrowTarget = GameObject.Find ("level1_head").transform.position; 
						}
					}

				}
				setTarget = true;
			}
			if (coll == null)
			{
				Destroy (gameObject);
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

	void Update()
	{
		arrowTarget = coll.gameObject.transform.position;
	}

	void FixedUpdate()
	{
		transform.position = Vector3.Lerp (transform.position, arrowTarget, Time.deltaTime * speed); 
	}
}
