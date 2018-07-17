using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime3D : MonoBehaviour {
	private Rigidbody rg;
	private SpriteRenderer spr;
    public GameObject sprite;
    public GameObject painter;
    public GameObject shadow;
    public bool omega = false;
    private bool merged = false;
    private bool isAbducted;
    private Transform offset;

    public Color baseColor;

	[SerializeField]
	public float speed;
	private Vector3 velocity;

	[SerializeField]
	private Vector3 inputMove = Vector3.zero;

	[SerializeField]
	private bool onSlime = false;
	[SerializeField]
	private string axisH, axisV;
	// Use this for initialization
	void Start () {
		rg = this.GetComponent<Rigidbody>();
		spr = this.GetComponentInChildren<SpriteRenderer>();

        sprite = this.transform.GetChild(0).gameObject;
        shadow = this.transform.GetChild(1).gameObject;
        painter = this.transform.GetChild(2).gameObject;

        baseColor = painter.GetComponent<SpawnTrail>().colr;

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
			case "SlimeB":
				axisH = "Horizontal4";
				axisV = "Vertical4";
				break;
		}

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Slime"))
        {
                Physics.IgnoreCollision(g.GetComponent<Collider>(), this.GetComponent<Collider>());
                //print("Ignored Slime");
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
            rg.AddForce(velocity, ForceMode.Force);
            if (Input.GetAxis(axisH) < 0) {
                spr.flipX = false;
			} else if (Input.GetAxis(axisH) > 0) {
                spr.flipX = true;
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

    private void OnTriggerEnter(Collider other)
    {
        
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigdsger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other) {
        if (other.tag == "Slime" && !omega && merged)
        {
            
            other.GetComponent<Slime3D>().omega = false;
            other.GetComponent<Slime3D>().sprite.SetActive(true);
            other.GetComponent<Slime3D>().shadow.SetActive(true);
            other.GetComponent<Slime3D>().painter.GetComponent<SpawnTrail>().working = true;

            this.transform.localScale *= 0.5f;
            painter.transform.localScale *= 2;
            painter.GetComponent<SpawnTrail>().size *= 0.5f;
            painter.GetComponent<SpawnTrail>().val *= 0.2f;

            painter.GetComponent<SpawnTrail>().colr = baseColor;
            sprite.GetComponent<SpriteRenderer>().color = baseColor;

            other.GetComponent<Slime3D>().painter.GetComponent<SpawnTrail>().colr = other.GetComponent<Slime3D>().baseColor;
            other.GetComponent<Slime3D>().sprite.GetComponent<SpriteRenderer>().color = other.GetComponent<Slime3D>().baseColor;

            merged = false;
            print("SlimeExit");
        }
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
			//print(other.gameObject.name);
			rg.AddForce(dir  * (3/ Mathf.Pow(dist, 2))  , ForceMode.Force);
		}
        /*
        if (merged)
        {
            rg.AddForce((other.transform.position - this.transform.position).normalized);
        }
        */
        if (other.tag == "Slime" && !omega && !merged)
        {
            other.GetComponent<Slime3D>().omega = true;
            merged = true;
            other.GetComponent<Slime3D>().sprite.SetActive(false);
            other.GetComponent<Slime3D>().shadow.SetActive(false);
            other.GetComponent<Slime3D>().painter.GetComponent<SpawnTrail>().working = false;

            this.transform.localScale *= 2;
            painter.transform.localScale *= 0.5f;
            painter.GetComponent<SpawnTrail>().size *= 2;
            painter.GetComponent<SpawnTrail>().val *= 5;

            painter.GetComponent<SpawnTrail>().colr = Color.Lerp(painter.GetComponent<SpawnTrail>().colr, other.GetComponent<Slime3D>().painter.GetComponent<SpawnTrail>().colr, 0.5f);
            sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(painter.GetComponent<SpawnTrail>().colr, other.GetComponent<Slime3D>().painter.GetComponent<SpawnTrail>().colr, 0.5f);

            print("SlimeEnter");
        }
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
		if (other.gameObject.tag == "Janitor")
		{
			if(other.gameObject.GetComponentInParent<Janitor3D>().isVacuuming)
			{
				isAbducted = true;
				this.GetComponent<CapsuleCollider>().enabled = false;
				painter.gameObject.SetActive(false);
				offset = GameObject.Find("Janitor Backpack").transform;
			}
		}
	}
}