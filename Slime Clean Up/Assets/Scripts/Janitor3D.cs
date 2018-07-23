﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor3D : MonoBehaviour {

    private Rigidbody rg;
    private CapsuleCollider col;
    private Sprite sprite;
    private SpriteRenderer spr;
    private Animator anim;

    [SerializeField]
    private Transform janitorBackpack;
    private float backpackX = 0.309f;
    private Transform janitorVacuum;
    private Transform painter;
    private Transform mySlime = null;
    private ParticleSystem dustTrail;
    private ParticleSystem dustStart;

    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private Vector3 lastMotion;
  
   // janitor inputs
    [SerializeField]
    private Vector3 inputMove;
    private bool inputVacuum;
    private bool inputSprint;

    // speeds of the janitor
    private float speed = 1;
    [SerializeField]
    public float crawlSpeed = 2;
    [SerializeField]
    public float walkSpeed = 8;
    [SerializeField]
    public float runSpeed = 20;


    // boolean states
    [SerializeField]
    private bool isSprinting;
    [SerializeField]
	public bool isVacuuming;
    [SerializeField]
    public bool abductedSlime;
    [SerializeField]
    public bool facingRight;

    private int layerMask;

    // Use this for initialization
    void Start() 
    {
        layerMask = 1 << 13;
        rg = this.GetComponent<Rigidbody>();
        col = this.GetComponent<CapsuleCollider>();
        spr = this.GetComponentInChildren<SpriteRenderer>();
        dustTrail = this.GetComponentsInChildren<ParticleSystem>()[0];
        dustStart = this.GetComponentsInChildren<ParticleSystem>()[1];
        janitorVacuum = GameObject.FindGameObjectWithTag("JanitorVacuum").transform;
        janitorBackpack = this.GetComponentsInChildren<Transform>()[6];
        anim = this.GetComponentInChildren<Animator>();
        painter = this.transform.Find("Painter");
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        GetPlayerInput();
        UpdateState();
    }

	/// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
	void FixedUpdate ()
    {
        rg.velocity = velocity;
	}

    void GetPlayerInput()
    {
        inputMove.x = Input.GetAxis("Horizontal");
        inputMove.z = Input.GetAxis("Vertical");

        inputVacuum = Input.GetButton("Fire1");
        inputSprint = Input.GetButton("Fire3");
    }

    void UpdateState()
    {
        if (inputSprint == true)
        {
            isSprinting = true;
            isVacuuming = false;
            janitorVacuum.gameObject.SetActive(false);
            painter.gameObject.SetActive(false);
            speed = runSpeed;
        }
        else if (inputVacuum == true)
        {
            isVacuuming = true;
            isSprinting = false;
            janitorVacuum.gameObject.SetActive(true);
            painter.gameObject.SetActive(true);
            speed = crawlSpeed;
        }
        else
        {
            isVacuuming = false;
            isSprinting = false;
            janitorVacuum.gameObject.SetActive(false);
            painter.gameObject.SetActive(true);
            speed = walkSpeed;
        }

        if (inputMove != Vector3.zero && !isSprinting)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
				spr.flipX = false;
				dustTrail.transform.rotation = Quaternion.Euler(0, 180f, 0);
                painter.localPosition = new Vector3(-0.8f, 0, -0.8f);
                painter.localRotation = Quaternion.Euler(0, 180, 0);
                janitorBackpack.localPosition = new Vector3(backpackX, janitorBackpack.localPosition.y, janitorBackpack.localPosition.z);
			}
            else if (Input.GetAxis("Horizontal") > 0)
            {
				spr.flipX = true;
				dustTrail.transform.rotation = Quaternion.Euler(0, 0, 0);
                painter.localPosition = new Vector3(0.8f, 0, -0.8f);
                painter.localRotation = Quaternion.Euler(0, 0, 0);
                janitorBackpack.localPosition = new Vector3(backpackX * -1, janitorBackpack.localPosition.y, janitorBackpack.localPosition.z);
            }

			if (dustTrail.isStopped)
            {
				dustTrail.Play();
			}

            anim.Play("JanitorRunRough");
            anim.speed = Mathf.Max(0.5f, Vector3.ClampMagnitude(inputMove, 1).magnitude);

            janitorVacuum.rotation = Quaternion.FromToRotation(transform.right, inputMove);
        } 
        
        if (isSprinting)
        {
                velocity = lastMotion * speed;
        }
        else
        {
            velocity = Vector3.ClampMagnitude(inputMove, 1.0f) * speed;
            lastMotion = Vector3.Normalize(inputMove);
        }

    }

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate()
	{
		if (velocity.magnitude < 0.05)
        {
			dustTrail.Stop();
            anim.Play("JanitorIdle");
		}
	}
    
    void AbductSlime(Transform slime)
    {
        abductedSlime = true;
        mySlime = slime;
        mySlime.GetComponent<Slime3D>().Abducted(this.transform);
    }

    void DepositSlime(Transform target)
    {
        mySlime.GetComponent<Slime3D>().Deposit(target);
        mySlime = null;
        abductedSlime = false;
    }

	/// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (abductedSlime && other.gameObject.name == "SlimeTanks"){
            DepositSlime(other.GetComponent<SlimeDeposit>().HoldSlime());
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Slime" && isVacuuming && !abductedSlime)
        {
            AbductSlime(other.gameObject.transform);
        }
    }
}
