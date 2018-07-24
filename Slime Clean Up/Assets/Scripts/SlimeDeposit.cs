using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeposit : MonoBehaviour {
	public static SlimeDeposit instance;
	private int numSlimes;
	[SerializeField]
	public Transform loc1, loc2, loc3, origin;
	[SerializeField]
	private bool loc1Used, loc2Used, loc3Used;
	[SerializeField]
	private List<Slime3D> mySlimes;
	// Use this for initialization
	void Start () 
	{
		loc1 = this.GetComponentsInChildren<Transform>()[1];
		loc2 = this.GetComponentsInChildren<Transform>()[2];
		loc3 = this.GetComponentsInChildren<Transform>()[3];
		origin = this.GetComponentsInChildren<Transform>()[4];

		instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public Transform HoldSlime()
	{
		if (!loc1Used) 
		{
			loc1Used = true;
			return loc1;
		}
		else if (!loc2Used) 
		{
			loc2Used = true;
			return loc2;
		}
		else if (!loc3Used) 
		{
			loc3Used = true;
			return loc3;
		}
		else 
		{
			return null;
		}
	} 

	public void FreeSlimes()
	{
		loc1Used = false;
		loc2Used = false;
		loc3Used = false;

		foreach (Slime3D item in mySlimes)
		{
			item.transform.position = Vector3.MoveTowards(this.origin.position, item.transform.position - this.origin.position, 1f);
			item.Freed();
		}
		mySlimes.Clear();
		StartingForce.instance.Explode();
	}

	public void SetSlime(Slime3D slime)
	{
		mySlimes.Add(slime);
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Slime")
		{

		}
	}
}
