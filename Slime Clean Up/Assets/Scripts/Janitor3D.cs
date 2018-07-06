using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor3D : MonoBehaviour {

	private Rigidbody rg;
	private Transform transform;
	private Sprite sprite;
	private SpriteRenderer spr;

	[SerializeField]
	public float speed;
	private Vector3 velocity;
	[SerializeField]
	private Vector3 inputMove;

	// Use this for initialization
	void Start () {
		rg = this.GetComponent<Rigidbody>();
		transform = this.GetComponent<Transform>();
		spr = this.GetComponentInChildren<SpriteRenderer>();
	}
	
	void Update () {
		inputMove.x = Input.GetAxis("Horizontal");
		inputMove.z = Input.GetAxis("Vertical");

		velocity = Vector3.ClampMagnitude(inputMove, 1.0f) * speed;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(inputMove != Vector3.zero){
			//rg.MovePosition(rg.position +  new Vector2(velocity * Time.fixedDeltaTime * Input.GetAxis("Horizontal"), velocity * Time.fixedDeltaTime * Input.GetAxis("Vertical")));
			rg.velocity = velocity;
			if(Input.GetAxis("Horizontal") < 0)
				spr.flipX = true;
			else if(Input.GetAxis("Horizontal") > 0)
				spr.flipX = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		print("I HIT OTHER");
		if (other.gameObject.tag.Equals("SlimeTrail")){
			Destroy(other.gameObject);
		}
	}
}
