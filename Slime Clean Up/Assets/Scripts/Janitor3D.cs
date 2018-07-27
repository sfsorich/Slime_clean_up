using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor3D : MonoBehaviour {

    // janitor components
    private Rigidbody rg;
    private CapsuleCollider col;
    private Sprite sprite;
    private SpriteRenderer spr;
    private Animator anim;

    // janitor sounds
    public AudioClip abductSound;
    public AudioClip depositSound;

    // janitor utilities
    [SerializeField]
    private Transform janitorBackpack;
    private float backpackX = 0.309f;
    private Transform janitorVacuum;
    [SerializeField]
    private Transform painter;
    private Slime3D mySlime = null;
     public float abductTime;

    private ParticleSystem dustTrail;
    private ParticleSystem dustStart;
  
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

    // janitor movement controls
    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private Vector3 lastMotion;
    [SerializeField]
    private string axisH, axisV, jumpButton, vacuumButton, sprintButton;

    // boolean states
    [SerializeField]
    private bool isSprinting;
    [SerializeField]
	public bool isVacuuming;
    [SerializeField]
    public bool abductedSlime;

    // Use this for initialization
    void Start() 
    {
        string name = this.name;
		switch(name)
        {
			case "Janitor3D":
				axisH = "Horizontal";
				axisV = "Vertical";
                jumpButton = "Jump1";
                vacuumButton = "Fire1";
                sprintButton = "Fire3";
				break;
			case "Janitor3D 2":
				axisH = "Horizontal2";
				axisV = "Vertical2";
                jumpButton = "Jump2";
                vacuumButton = "Fire1_2";
                sprintButton = "Fire3_2";
				break;
		}

        rg = this.GetComponent<Rigidbody>();
        col = this.GetComponent<CapsuleCollider>();
        spr = this.GetComponentInChildren<SpriteRenderer>();
        anim = this.GetComponentInChildren<Animator>();
        painter = this.transform.Find("Painter");

        dustTrail = this.GetComponentsInChildren<ParticleSystem>()[0];
        dustStart = this.GetComponentsInChildren<ParticleSystem>()[1];

        janitorBackpack = this.GetComponentsInChildren<Transform>()[6];
        janitorVacuum = this.GetComponentsInChildren<Transform>()[7];
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
        inputMove.x = Input.GetAxis(axisH);
        inputMove.z = Input.GetAxis(axisV);

        inputVacuum = Input.GetButton(vacuumButton);
        inputSprint = Input.GetButton(sprintButton);
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
            if (Input.GetAxis(axisH) < 0)
            {
				spr.flipX = false;
				dustTrail.transform.rotation = Quaternion.Euler(0, 180f, 0);
                painter.localPosition = new Vector3(-0.8f, 0, -0.8f);
                painter.localRotation = Quaternion.Euler(0, 180, 0);
                janitorBackpack.localPosition = new Vector3(backpackX, janitorBackpack.localPosition.y, janitorBackpack.localPosition.z);
			}
            else if (Input.GetAxis(axisH) > 0)
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

            anim.speed = Mathf.Max(0.5f, Vector3.ClampMagnitude(inputMove, 1).magnitude);
            anim.Play("JanitorRunRough");
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

        if (abductedSlime || abductTime < 0)
            abductTime += Time.deltaTime;

        if (abductTime > 6f)
        {
            FreeSlime();
        }
	}
    
    void AbductSlime(Slime3D slime)
    {
        abductedSlime = true;
        mySlime = slime;
        mySlime.Abducted(this.transform);
        SoundManager.instance.PlaySingle(abductSound);
        abductTime = 0;
    }

    void DepositSlime(Transform target)
    {
        mySlime.Deposit(target);
        SlimeDeposit.instance.SetSlime(mySlime);
        mySlime = null;
        abductedSlime = false;
        SoundManager.instance.PlaySingle(depositSound);
    }

    void FreeSlime()
    {   
        mySlime.Freed();
        mySlime.transform.position = Vector3.MoveTowards(this.transform.position, janitorBackpack.position, 5f);
        mySlime.rg.AddExplosionForce(100f, this.transform.position, 100f, 0f, ForceMode.Force);
        mySlime = null;
        abductedSlime = false;
        abductTime = -2f;
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
        if (other.gameObject.tag == "Slime" && isVacuuming && !abductedSlime && abductTime >= 0)
        {
            AbductSlime(other.gameObject.GetComponent<Slime3D>());
        }
    }
}
