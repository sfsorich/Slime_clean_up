using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManegerMenu : MonoBehaviour
{
    //this exists just to make sure time scale is on when heading back to menu
    private GameObject SlimeCounter;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        SlimeCounter = GameObject.Find("InfoStorage");
    }

    // Update is called once per frame
    void Update()
    {

    }
    //to load or restart a scene use LoadSceneOnClick instead
    public void SlimeSet(int SlimeCount)
    {
        SlimeCounter.GetComponent<InfoTransfer>().slimeNum = SlimeCount;
        DontDestroyOnLoad(SlimeCounter);
    }
}
