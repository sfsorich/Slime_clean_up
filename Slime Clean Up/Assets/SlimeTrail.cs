using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrail : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// OnTriggerStay is called once per frame for every Collider other
	/// that is touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.tag.Equals("SlimeTrail")){
			Destroy(this);
		}
	}
}
