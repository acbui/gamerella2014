using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	private Vector3 player;
	private Vector2 playerDirection;
	private float deltaX;
	private float deltaY;
	public float speed = 1.0f;
	private int wall;
	private float distance;
	private bool wait;
	private float waitTime;

	// Use this for initialization
	void Start () 
	{
		wall = 1 << 8;

		wait = false;
		waitTime = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		player = GameObject.Find ("Player").transform.position;

		distance = Vector2.Distance (player, transform.position);

		if (waitTime > 0)
		{
			waitTime -= Time.deltaTime;
		}
		else
		{
			wait = false;
		}

		if (distance < 10 && !wait)
		{
			deltaX = player.x - transform.position.x;
			deltaY = player.y - transform.position.y;
			playerDirection = new Vector2 (deltaX, deltaY);
			
			if (!Physics2D.Raycast (transform.position, playerDirection, 5, wall))
			{
				rigidbody2D.AddForce (playerDirection.normalized * speed);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			wait = true;
			waitTime = 5;
		}
	}

	public bool isWaiting ()
	{
		return wait;
	}
}
