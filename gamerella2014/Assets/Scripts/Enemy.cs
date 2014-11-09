using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	private Vector3 player;
	private Vector2 playerDirection;
	private float deltaX;
	private float deltaY;
	public float speed = 1.0f;
	private int obstacle;
	private float distance;
	private bool attack;
//	private float attackTime;
	private bool flee;
	private float fleeTime;

	public enum EnemyClass
	{
		Warrior,
		Archer,
		Mage
	}
	public EnemyClass enemyClass;

	// Use this for initialization
	void Start () 
	{
		obstacle = 1 << 8;

		attack = false;
//		attackTime = 0;

		flee = false;
		fleeTime = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		player = GameObject.Find ("Player").transform.position;

		distance = Vector2.Distance (player, transform.position);

//		if (attackTime > 0)
//		{
//			attackTime -= Time.deltaTime;
//		}
//		else
//		{
//			attack = false;
//		}

		if (fleeTime > 0)
		{
			fleeTime -= Time.deltaTime;
			rigidbody2D.AddForce (-playerDirection.normalized * speed * 2.0f);
		}
		else
		{
			flee = false;
		}

		if (enemyClass == EnemyClass.Warrior)
		{
			if (distance < 10 && distance != 0 && !attack && !flee)
			{
				deltaX = player.x - transform.position.x;
				deltaY = player.y - transform.position.y;
				playerDirection = new Vector2 (deltaX, deltaY);
				
				if (!Physics2D.Raycast (transform.position, playerDirection, 5, obstacle))
				{
					rigidbody2D.AddForce (playerDirection.normalized * speed);
				}
			}
		}

		if (enemyClass == EnemyClass.Archer || enemyClass == EnemyClass.Mage)
		{
			if (distance < 10 && distance >= 5 && distance != 0 && !attack && !flee)
			{
				deltaX = player.x - transform.position.x;
				deltaY = player.y - transform.position.y;
				playerDirection = new Vector2 (deltaX, deltaY);
				
				if (!Physics2D.Raycast (transform.position, playerDirection, 5, obstacle))
				{
					rigidbody2D.AddForce (playerDirection.normalized * speed);
				}
			}

			if (distance <= 7 && distance >= 5 && distance != 0 && !Physics2D.Raycast (transform.position, playerDirection, 5, obstacle))
			{
				attack = true;
			}
			else
				attack = false;
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "BossSwipe")
		{
			Debug.Log ("Swiper no swiping");
			flee = true;
			fleeTime = 2;
		}
	}

	void OnCollisionStay2D (Collision2D collision)
	{
		if (enemyClass == EnemyClass.Warrior)
		{
			if (collision.gameObject.tag == "Player")
			{
				attack = true;
//				attackTime = 3;
			}
			else
			{
				attack = false;
			}
		}
	}

	void OnCollisionExit2D (Collision2D collision)
	{
		
	}

	public bool isAttacking ()
	{
		return attack;
	}
}
