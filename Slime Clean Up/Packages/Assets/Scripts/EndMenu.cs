using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour {

	private int numSlimeG;
	private int numSlimeR;
	private int numSlimeB;

	private Text numSlimeText;


	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		numSlimeG = GameObject.FindGameObjectsWithTag("Brush").Length;
		numSlimeText = this.GetComponentInChildren<Text>();
		StartCoroutine(CountSlimes());
	}

	IEnumerator CountSlimes(){
		for(float i = 0; i < 2f; i+=0.01f){
			//numSlimeText.text = i.ToString();
			//numSlimeText.text = Mathf.CeilToInt(Mathf.Lerp(0, numSlimeG, i)).ToString();
			numSlimeText.text = Mathf.CeilToInt(Easing.QuintEaseOut(i, 0, numSlimeG, 2)).ToString();
			yield return new WaitForSeconds(0.01f);
		}
	}
}
