using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime3D : MonoBehaviour 
{
	private Rigidbody rg;
	private SpriteRenderer spr;

	[SerializeField]
	public float speed;
	private Vector3 velocity;

	[SerializeField]
	private Vector3 inputMove = Vector3.zero;

	[SerializeField]
	private string axisH, axisV;


	[SerializeField]
	private bool isAbducted;
	private Transform offset;
	[SerializeField]
	private bool onSlime = false;

	// Use this for initialization
	void Start () 
	{
		rg = this.GetComponent<Rigidbody>();
		spr = this.GetComponentInChildren<SpriteRenderer>();

		string name = this.name;
		switch(name)
		{
			case "SlimeG":
				axisH = "Horizontal2";
				axisV = "Vertical2";
				break;
			case "SlimeR":
				axisH = "Horizontal3";
				axisV = "Vertical3";
				break;
			case "SlimeB":
				axisH = "Horizontal4";
				axisV = "Vertical4";
				break;
		}

	}
	
	void Update () 
	{
		inputMove.x = Input.GetAxis(axisH);
		inputMove.z = Input.GetAxis(axisV);

		velocity = Vector3.ClampMagnitude(inputMove, 1.0f) * speed;
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (!isAbducted)
		{
			if (inputMove != Vector3.zero)
			{
				//rg.MovePosition(rg.position +  new Vector2(velocity * Time.fixedDeltaTime * Input.GetAxis("Horizontal"), velocity * Time.fixedDeltaTime * Input.GetAxis("Vertical")));
				rg.AddForce(velocity, ForceMode.Force);
				if (Input.GetAxis("Horizontal2") < 0) 
				{
					spr.flipX = false;
				} 
				else if (Input.GetAxis("Horizontal2") > 0)
				{
					spr.flipX = true;
				}
			}
		}
	}

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate()
	{
		if (isAbducted)
		{
			this.GetComponent<CapsuleCollider>().enabled = false;
			this.GetComponentInChildren<SpawnTrail>().enabled = false;
			this.transform.position = offset.position;
	
		}
	}

	/// <summary>
	/// OnTriggerExit is called when the Collider other has stopped touching the trigdsger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerExit(Collider other) 
	{

	}

	/// <summary>
	/// OnTriggerStay is called once per frame for every Collider other
	/// that is touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "JanitorVacuum"){
			float dist = Vector3.Distance(this.transform.position, other.gameObject.GetComponentInParent<Transform>().position);
			Vector3 dir = other.GetComponentInParent<Transform>().position - this.transform.position;
			rg.AddForce(dir  * (3/ Mathf.Pow(dist, 2))  , ForceMode.Force);
		}
	}

	 /// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "SlimeGrate")
		{
			Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		}
		if (other.gameObject.tag == "Janitor")
		{
			if(other.gameObject.GetComponentInParent<Janitor3D>().isVacuuming)
			{
				isAbducted = true;
				offset = GameObject.Find("Janitor Backpack").transform;
			}
		}
	}
}