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
	private string axisH, axisV;
	// Use this for initialization
	void Start () {
		rg = this.GetComponent<Rigidbody>();
		transform = this.GetComponent<Transform>();
		spr = this.GetComponentInChildren<SpriteRenderer>();

		string name = this.name;
		switch(name){
			case "SlimeG":
				axisH = "Horizontal2";
				axisV = "Vertical2";
				break;
			case "SlimeR":
				axisH = "Horizontal3";
				axisV = "Vertical3";
				break;
		}
	}
	
	void Update () {
		inputMove.x = Input.GetAxis(axisH);
		inputMove.z = Input.GetAxis(axisV);

		velocity = Vector3.ClampMagnitude(inputMove, 1.0f) * speed;
	}
	// Update is called once per frame
	void FixedUpdate () {
        if (inputMove != Vector3.zero)
        {
            //rg.MovePosition(rg.position +  new Vector2(velocity * Time.fixedDeltaTime * Input.GetAxis("Horizontal"), velocity * Time.fixedDeltaTime * Input.GetAxis("Vertical")));
            rg.velocity = velocity;
            if (Input.GetAxis("Horizontal2") < 0)
                this.transform.localScale = new Vector3(-1, 1, 1);
            else if (Input.GetAxis("Horizontal2") > 0)
                this.transform.localScale = new Vector3(1, 1, 1);
        }
	}

	/// <summary>
	/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerExit(Collider other) {

	}

	/// <summary>
	/// OnTriggerStay is called once per frame for every Collider other
	/// that is touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerStay(Collider other)
	{

	}

	 /// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "SlimeGrate"){
			Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
}