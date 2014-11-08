using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour 
{
	private Animator anim;
	
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (rigidbody2D.velocity.y > 0)
		{
			anim.SetBool ("WalkBack", true);
			anim.SetBool ("Back", true);
			anim.SetBool ("WalkFront", false);
			anim.SetBool ("Front", false);
		}
		
		if (rigidbody2D.velocity.y < 0)
		{
			anim.SetBool ("WalkBack", false);
			anim.SetBool ("Back", false);
			anim.SetBool ("WalkFront", true);
			anim.SetBool ("Front", true);
		}

		if (rigidbody2D.velocity.magnitude == 0.0f)
		{
			anim.SetBool ("WalkBack", false);
			anim.SetBool ("WalkFront", false);
		}

		if (gameObject.GetComponent <Enemy> ().isWaiting ())
		{
			anim.SetBool ("Attack", true);
		}
		else
		{
			anim.SetBool ("Attack", false);
		}
	}
}
