using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor : MonoBehaviour {

	private Rigidbody2D rg;
	private Transform transform;
	private Sprite sprite;
	private SpriteRenderer spr;

	[SerializeField]
	public float speed;
	private Vector2 velocity;
	[SerializeField]
	private Vector2 inputMove;

	// Use this for initialization
	void Start () {
		rg = this.GetComponent<Rigidbody2D>();
		transform = this.GetComponent<Transform>();
		spr = this.GetComponentInChildren<SpriteRenderer>();
	}
	
	void Update () {
		inputMove.x = Input.GetAxis("Horizontal");
		inputMove.y = Input.GetAxis("Vertical");

		velocity = Vector2.ClampMagnitude(inputMove, 1.0f) * speed;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(inputMove != Vector2.zero){
			//rg.MovePosition(rg.position +  new Vector2(velocity * Time.fixedDeltaTime * Input.GetAxis("Horizontal"), velocity * Time.fixedDeltaTime * Input.GetAxis("Vertical")));
			rg.velocity = velocity;
			if(Input.GetAxis("Horizontal") < 0)
				spr.flipX = true;
			else
				spr.flipX = false;
		}
	}
}
