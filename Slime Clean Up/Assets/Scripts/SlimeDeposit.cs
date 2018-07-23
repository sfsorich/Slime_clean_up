using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeposit : MonoBehaviour {

	private int numSlimes;
	[SerializeField]
	public Transform loc1, loc2, loc3;
	[SerializeField]
	private bool loc1Used, loc2Used, loc3Used;
	// Use this for initialization
	void Start () 
	{
		loc1 = this.GetComponentsInChildren<Transform>()[1];
		loc2 = this.GetComponentsInChildren<Transform>()[2];
		loc3 = this.GetComponentsInChildren<Transform>()[3];
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
}
