using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour {

	private float numSlimeG;
	private int numSlimeR;
	private int numSlimeB;

	private Text numSlimeText;
	private Text winnerText;

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
		numSlimeG = 0;
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Brush"))
        {
            numSlimeG += g.GetComponent<DeleteBrush>().val;
        }
		numSlimeText = this.GetComponentInChildren<Text>();
		winnerText = this.GetComponentsInChildren<Text>()[1];
		winnerText.text = " ";
		StartCoroutine(CountSlimes());
	}

	IEnumerator CountSlimes(){
		for(float i = 0; i < 2f; i+=0.01f)
		{
			numSlimeText.text = Mathf.CeilToInt(Easing.QuintEaseOut(i, 0, numSlimeG, 2f)).ToString();
			yield return new WaitForSecondsRealtime(0.01f);
			if (numSlimeText.text == numSlimeG.ToString())
			{
				break;
			}
		}

		for (int i = 0; i < 30; i+=1)
		{
			if ( i % 2 == 0) 
			{
				winnerText.text = "JANITOR";
			}
			else
			{
				winnerText.text = "SLIMES";
			}
			
			yield return new WaitForSecondsRealtime(0.1f);
		}

		if (numSlimeG < GameObject.FindObjectOfType<GameManage>().winAmount)
		{
			winnerText.text = "JANITOR";
		}
		else
		{
			winnerText.text = "SLIMES";
		}
	}
}
