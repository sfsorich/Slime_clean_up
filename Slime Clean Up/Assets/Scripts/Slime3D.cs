using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime3D : MonoBehaviour {
	private Rigidbody rg;
	private Transform transform;
	private Sprite sprite;
	private SpriteRenderer spr;

	[SerializeField]
	public float speed;
	private Vector3 velocity;
	[SerializeField]
	private Vector3 inputMove = Vector3.zero;
	public Transform slime;

	[SerializeField]
	private bool onSlime = false;
	[SerializeField]
	private int moveCount = 0;

	// Use this for initialization
	void Start () {
		rg = this.GetComponent<Rigidbody>();
		transform = this.GetComponent<Transform>();
		spr = this.GetComponentInChildren<SpriteRenderer>();
	}
	
	void Update () {
		inputMove.x = Input.GetAxis("Horizontal2");
		inputMove.z = Input.GetAxis("Vertical2");

		velocity = Vector3.ClampMagnitude(inputMove, 1.0f) * speed;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(inputMove != Vector3.zero){
			//rg.MovePosition(rg.position +  new Vector2(velocity * Time.fixedDeltaTime * Input.GetAxis("Horizontal"), velocity * Time.fixedDeltaTime * Input.GetAxis("Vertical")));
			rg.velocity = velocity;
			if(Input.GetAxis("Horizontal2") > 0)
				spr.flipX = true;
			else if (Input.GetAxis("Horizontal2") < 0)
				spr.flipX = false;
			++moveCount;
		}
		if (moveCount > 10 && onSlime == false){
			Instantiate(slime, this.transform.position - 0.1f*velocity - new Vector3(0,  0.2f, 0), Quaternion.Euler(90f, 0f ,0f));
			moveCount = 0;
		}
	}

	/// <summary>
	/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerExit(Collider other) {
		if (other.tag.Equals("SlimeTrail")){
			onSlime = false;
		}
	}

	/// <summary>
	/// OnTriggerStay is called once per frame for every Collider other
	/// that is touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals("SlimeTrail")){
			onSlime = true;
		}
	}
}