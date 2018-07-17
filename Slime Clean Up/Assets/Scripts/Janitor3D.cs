using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor3D : MonoBehaviour {

    private Rigidbody rg;
    private Transform painter;
    private Sprite sprite;
    private SpriteRenderer spr;
    private Animator anim;

    [SerializeField]
    private Transform janitorVaccum;
    private ParticleSystem dustTrail;
    private ParticleSystem dustStart;

    //[SerializeField]
    private float speed = 8;
    private Vector3 velocity;
    [SerializeField]
    private Vector3 inputMove;

    // speeds of the janitor


    // boolean states
    private bool sprintEnable;
    [SerializeField]
	public bool isVacuuming;


    // Use this for initialization
    void Start() 
    {
        rg = this.GetComponent<Rigidbody>();
        //transform = this.GetComponent<Transform>();
        spr = this.GetComponentInChildren<SpriteRenderer>();
        dustTrail = this.GetComponentsInChildren<ParticleSystem>()[0];
        dustStart = this.GetComponentsInChildren<ParticleSystem>()[1];
        janitorVaccum = this.GetComponentsInChildren<Transform>()[7];
        anim = this.GetComponentInChildren<Animator>();
        painter = this.transform.Find("Painter");
    }

    void Update() 
    {
        inputMove.x = Input.GetAxis("Horizontal");
        inputMove.z = Input.GetAxis("Vertical");

        if (Input.GetButton("Fire1"))
		{
			isVacuuming = true;
			janitorVaccum.gameObject.SetActive(true);
			this.speed = 2;
		} 
        else 
        {
			isVacuuming = false;
			janitorVaccum.gameObject.SetActive(false);
			this.speed = 8;
		}

        velocity = Vector3.ClampMagnitude(inputMove, 1.0f) * speed;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {

        if (inputMove != Vector3.zero)
        {
			if (dustTrail.isStopped)
            {
				dustTrail.Play();
			}

            anim.Play("JanitorRunRough");

            rg.velocity = velocity;
            janitorVaccum.rotation = Quaternion.FromToRotation(transform.right, inputMove);

            if (Input.GetAxis("Horizontal") < 0)
            {
				spr.flipX = false;
				dustTrail.transform.rotation = Quaternion.Euler(0, 180f, 0);
                painter.localPosition = new Vector3(-0.75f, -0.45f, -0.8f);
			}
            else if (Input.GetAxis("Horizontal") > 0)
            {
				spr.flipX = true;
				dustTrail.transform.rotation = Quaternion.Euler(0, 0, 0);
                painter.localPosition = new Vector3(0.75f, -0.45f, -0.8f);
            }
        } 
	}

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate()
	{
		if (rg.velocity.magnitude < 0.15){
			dustTrail.Stop();
            anim.Play("JanitorIdle");
		}
	}
    
	void OnTriggerEnter(Collider other)
	{
		
	}
}
