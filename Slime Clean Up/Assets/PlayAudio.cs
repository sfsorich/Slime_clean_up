using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {

	public AudioSource sound;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		sound = this.GetComponent<AudioSource>();
		sound.Play(0);
	}
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		sound.pitch = Random.value + 1 ;
		sound.Play(0);
	}
}
