using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	

	public float moveForce=0;

	private float h,v;
	private Vector2 direction;
	private bool facingRight;

	//private Animator animator;

	void Awake(){
		direction = new Vector2 (0,0);
		facingRight = true;
	}

	void FixedUpdate(){

		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");



		direction.Set (h , v);
		direction.Normalize();
		//anim.SetBool ("Moving", direction.magnitude != 0);

		//rigidbody2D.AddForce( direction*moveForce);

		rigidbody2D.AddForce(direction*moveForce);

		if (h < 0 && facingRight)
						Flip ();
		if (h > 0 && !facingRight)
						Flip ();

	}


	void Flip(){
			facingRight = !facingRight;
		Vector2 scale = transform.localScale;
		scale.x *=-1;
		transform.localScale = scale;
	}
}
