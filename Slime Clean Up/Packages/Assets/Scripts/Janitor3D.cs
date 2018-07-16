using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor3D : MonoBehaviour {

	private Rigidbody rg;
	//private Transform transform;
	private Sprite sprite;
	private SpriteRenderer spr;
	private Animator anim;
	[SerializeField]
	private Transform janitorVaccum;
	private ParticleSystem dustTrail;
	private ParticleSystem dustStart;

	[SerializeField]
	public float speed;
	private Vector3 velocity;
	[SerializeField]
	private Vector3 inputMove;
	[SerializeField]
	public bool canMove;
	[SerializeField]
	public bool isVacuuming;

	// Use this for initialization
	void Start () 
	{
		rg = this.GetComponent<Rigidbody>();
		//transform = this.GetComponent<Transform>();
		spr = this.GetComponentInChildren<SpriteRenderer>();
		dustTrail = this.GetComponentsInChildren<ParticleSystem>()[0];
		dustStart = this.GetComponentsInChildren<ParticleSystem>()[1];
		janitorVaccum = this.GetComponentsInChildren<Transform>()[7];
		anim = this.GetComponentInChildren<Animator>();
    }
	
	void Update ()
	{
		inputMove.x = Input.GetAxis("Horizontal");
		inputMove.z = Input.GetAxis("Vertical");

		velocity = Vector3.ClampMagnitude(inputMove, 1.0f) * speed;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

		if (Input.GetButton("Fire1"))
		{
			isVacuuming = true;
			janitorVaccum.gameObject.SetActive(true);
			this.speed = 2;
		} else {
			isVacuuming = false;
			janitorVaccum.gameObject.SetActive(false);
			this.speed = 8;
		}

        if (inputMove != Vector3.zero)
        {
			anim.Play("JanitorRunRough");
			if (dustTrail.isStopped)
			{
				dustTrail.Play();
				dustStart.Play();
			}
            //rg.MovePosition(rg.position +  new Vector2(velocity * Time.fixedDeltaTime * Input.GetAxis("Horizontal"), velocity * Time.fixedDeltaTime * Input.GetAxis("Vertical")));
            
			rg.velocity = velocity;
			janitorVaccum.rotation = Quaternion.FromToRotation(transform.right, inputMove);

            if (Input.GetAxis("Horizontal") < 0)
			{
				spr.flipX = false;
				dustTrail.transform.rotation = Quaternion.Euler(0, 180f, 0);
			}
            else if (Input.GetAxis("Horizontal") > 0)
			{
				spr.flipX = true;
				dustTrail.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
        } 

	}

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate()
	{
		if (rg.velocity.magnitude < 0.25){
			dustTrail.Stop();
			anim.Play("JanitorIdle");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		
	}
}
