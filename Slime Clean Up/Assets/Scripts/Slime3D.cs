using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime3D : MonoBehaviour {
	public Rigidbody rg;
	private SpriteRenderer spr;
    public GameObject sprite;
    public GameObject painter;
    public GameObject shadow;
    private bool isAbducted;
    private Transform offset;

    //merging
    public int priority = 0;
    public bool omega = false;
    public bool merged = false;
    public Slime3D mergedAlpha;
    public Slime3D mergedOmega;
    public bool justUnMerged = false;
    public bool justMerged = false;

    public Color baseColor;

	[SerializeField]
	public float speed;
	public Vector3 velocity;

	[SerializeField]
	public Vector3 inputMove = Vector3.zero;

	[SerializeField]
	private bool onSlime = false;
	[SerializeField]
	private string axisH, axisV, jump;
    private int jumping = 0;
	private int height = 0;

	// Use this for initialization
	void Start () {
		rg = this.GetComponent<Rigidbody>();
		spr = this.GetComponentInChildren<SpriteRenderer>();

        sprite = this.transform.GetChild(0).gameObject;
        shadow = this.transform.GetChild(1).gameObject;
        painter = this.transform.GetChild(2).gameObject;

        baseColor = painter.GetComponent<SpawnTrail>().colr;

        string name = this.name;
		switch(name)
        {
			case "SlimeG":
				axisH = "Horizontal2";
				axisV = "Vertical2";
                jump = "Jump2";
                priority = 1;
				break;
			case "SlimeR":
				axisH = "Horizontal3";
				axisV = "Vertical3";
                jump = "Jump3";
                priority = 2;
                break;
			case "SlimeB":
				axisH = "Horizontal4";
				axisV = "Vertical4";
                jump = "Jump4";
                priority = 3;
                break;
		}

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Slime"))
        {
                Physics.IgnoreCollision(g.GetComponent<Collider>(), this.GetComponent<Collider>());
                //print("Ignored Slime");
        }
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("SlimeGrate"))
        {
            Physics.IgnoreCollision(g.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
	}
	
	void Update () {
		inputMove.x = Input.GetAxis(axisH);
		inputMove.z = Input.GetAxis(axisV);

		if (Input.GetButton(jump) && !merged)
		{
			this.GetComponent<Rigidbody>().drag = 5;
			float scaleX = this.transform.localScale.x + (inputMove.x != 0 ? .01f : 0);
			if (scaleX > 1.5)
			{
				scaleX = 1.5f;
			}
			float scaleY = this.transform.localScale.y + (inputMove.y != 0 ? .01f : 0);
			if (scaleY > 1.5)
			{
				scaleY = 1.5f;
			}
			float scaleZ = this.transform.localScale.z - .01f;
			if(scaleZ < .5)
			{
				scaleZ = .5f;
			}

			inputMove.x = 0;
			inputMove.z = 0;
			jumping++;
			this.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
		}
		if (Input.GetButtonUp(jump) && !merged)
		{
			this.transform.localScale = new Vector3(1, 1, 1);
			this.GetComponent<Rigidbody>().drag = 0.5f;
			if(jumping > 30)
			{
				height = 1;
				if(jumping > 80)
				{
					jumping = 80;
				}
			}
			else
			{
				jumping = 0;
			}
		}
		if(height > 0){
			velocity = Vector3.ClampMagnitude(inputMove, 1.0f) * speed * 10;

			if(jumping > 0)
			{
				height += (jumping - height)/10 + 3;
				if(height >= jumping)
				{
					jumping *= -1;
				}
			}
			else
			{
				height -= ((0 - jumping) - height)/10 + 3;
				if (height <= 0){
					jumping = 0;
					height = 0;
				}
			}

			Vector3 offset = new Vector3(this.transform.position.x - .03f, this.transform.position.y + (height * .05f), this.transform.position.z + 0.11f);
			this.sprite.transform.SetPositionAndRotation(offset, this.sprite.transform.rotation);
		}
		else
		{
			velocity = Vector3.ClampMagnitude(inputMove, 1.0f) * speed;
		}

        if (merged)
        {
            mergedOmega.rg.position = mergedAlpha.rg.position;
        }
	}

	// Update is called once per frame
	void FixedUpdate () {
        if (inputMove != Vector3.zero)
        {
            //rg.MovePosition(rg.position +  new Vector2(velocity * Time.fixedDeltaTime * Input.GetAxis("Horizontal"), velocity * Time.fixedDeltaTime * Input.GetAxis("Vertical")));

            if (!merged)
            {
                rg.AddForce(velocity, ForceMode.Force);

                if (Input.GetAxis(axisH) < 0)
                {
                    spr.flipX = false;
                }
                else if (Input.GetAxis(axisH) > 0)
                {
                    spr.flipX = true;
                }
            }

            if (merged)
            {
                Vector3 mergeInput = mergedAlpha.inputMove + mergedOmega.inputMove;

                mergedAlpha.rg.AddForce(mergeInput * (speed / 3), ForceMode.Force);

                if (mergeInput.x < 0)
                {
                    mergedAlpha.spr.flipX = false;
                }
                else if (mergeInput.x > 0)
                {
                    mergedAlpha.spr.flipX = true;
                }

                if (Vector3.Dot(mergedOmega.inputMove, mergedAlpha.inputMove) < 0 && !justMerged)
                {
                    UnMerge();
                }


            }

            
        }
	}

    void LateUpdate()
	{
		if (isAbducted)
		{
			this.transform.position = offset.position;
		}
	}

    public void Abducted(Transform jan)
    {
        //unmerge
        if (merged)
        {
            mergedAlpha.UnMerge();
            mergedOmega.UnMerge();
        }

        isAbducted = true;
        this.GetComponent<CapsuleCollider>().enabled = false;
        painter.gameObject.SetActive(false);
        offset = jan.transform.Find("Janitor Backpack").transform;
        spr.sortingOrder = 11;
    }

    public void Deposit(Transform target)
    {
        if (target != null)
        {
            offset = target;
            spr.sortingOrder = 8;
        }
        else
            Debug.LogError("Slime tanks are full, null transform passed");
    }

    public void Freed()
    {
        isAbducted = false;
        this.GetComponent<CapsuleCollider>().enabled = true;
        painter.gameObject.SetActive(true);
        offset =null;
        spr.sortingOrder = 8;
    }

    public void Merge(Collider other)
    {
        mergedAlpha = this;
        mergedOmega = other.GetComponent<Slime3D>();

        mergedOmega.mergedAlpha = this;
        mergedOmega.mergedOmega = mergedOmega;

        mergedOmega.rg.position = mergedAlpha.rg.position;
        mergedAlpha.rg.velocity = Vector3.zero;
        mergedOmega.rg.isKinematic = true;

        mergedOmega.merged = true;
        mergedAlpha.merged = true;

        mergedOmega.sprite.SetActive(false);
        mergedOmega.shadow.SetActive(false);
        mergedOmega.painter.GetComponent<SpawnTrail>().working = false;

        this.transform.localScale = new Vector3(2, 2, 2);
        painter.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        painter.GetComponent<SpawnTrail>().size = 6;
        painter.GetComponent<SpawnTrail>().val = 5;

        painter.GetComponent<SpawnTrail>().colr = Color.Lerp(painter.GetComponent<SpawnTrail>().colr, other.GetComponent<Slime3D>().painter.GetComponent<SpawnTrail>().colr, 0.5f);
        sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(painter.GetComponent<SpawnTrail>().colr, other.GetComponent<Slime3D>().painter.GetComponent<SpawnTrail>().colr, 0.5f);
    }

    public void UnMerge()
    {
        painter.GetComponent<SpawnTrail>().colr = baseColor;
        sprite.GetComponent<SpriteRenderer>().color = baseColor;

        this.transform.localScale = new Vector3(1, 1, 1);
        painter.transform.localScale = new Vector3(1, 1, 1);
        painter.GetComponent<SpawnTrail>().size = 3;
        painter.GetComponent<SpawnTrail>().val = 1;

        rg.isKinematic = false;

        sprite.SetActive(true);
        shadow.SetActive(true);
        painter.GetComponent<SpawnTrail>().working = true;

        merged = false;

        

        justUnMerged = true;
        StartCoroutine(WaitSec(1));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Slime" && !merged && !isAbducted && !justUnMerged && !other.GetComponent<Slime3D>().merged)
        {
            if(this.priority < other.GetComponent<Slime3D>().priority)
            {
                Merge(other);
                print("Merging " + this.name);
            }
            justMerged = true;
            StartCoroutine(WaitSec(1));
        }
        else if (other.tag == "SlimeDeposit" && !isAbducted)
        {
            other.GetComponent<SlimeDeposit>().FreeSlimes();
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigdsger.
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
		if (other.tag == "JanitorVacuum")
        {
			float dist = Vector3.Distance(this.transform.position, other.gameObject.GetComponentInParent<Transform>().position);
			Vector3 dir = other.GetComponentInParent<Transform>().position - this.transform.position;
			rg.AddForce(dir  * (6/ Mathf.Pow(dist, 1))  , ForceMode.Force);
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
	}

    private IEnumerator WaitSec(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (justMerged)
            justMerged = false;
        if (justUnMerged)
            justUnMerged = false;
        yield return new WaitForEndOfFrame();
    }
}