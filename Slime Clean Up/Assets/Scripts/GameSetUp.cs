using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour {

    GameObject[] SlimeObjects;

    private GameObject SlimeSetUp;
    private int ActiveSlimes;

    // Use this for initialization
    void Start () {
        SlimeObjects = GameObject.FindGameObjectsWithTag("Slime");
        SlimeSetUp = GameObject.Find("InfoStorage");
        ActiveSlimes = SlimeSetUp.GetComponent<InfoTransfer>().slimeNum;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public int GetNum()
    {
        return ActiveSlimes;
    }
    void SettingActiveSlimes()
    {
        if (ActiveSlimes == 1)
        {
            SlimeObjects[1].SetActive(false);
            SlimeObjects[2].SetActive(false);
            Debug.Log("1 slime active");
        }
        else if(ActiveSlimes == 2)
        {
            SlimeObjects[2].SetActive(false);
            Debug.Log("2 slimes active");
        }
        else
        {
            Debug.Log("error active slimes not set");
        }
    }
}
