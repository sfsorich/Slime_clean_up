using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    GameObject[] pauseObjects;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        HidePaused();
    }

    // Update is called once per frame
    void Update()
    {
        //uses the esc or start button to pause and unpause the game
        if ( (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start")) && !GameManage.endBool )//not sure if these will work with controllers but keeping || Input.GetButtonDown("Start") || Input.GetButtonDown("Start_2")
        {
            PauseControl();
        }
        
    }
    //to load or restart a scene use LoadSceneOnClick
    //controls the pausing of the scene
    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HidePaused();
        }
    }
    //shows objects with ShowOnPause tag
    public void ShowPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            //Debug.Log("displaying pauseobject");
            g.SetActive(true);
        }
        Cursor.visible = true;
    }
    //hides objects with ShowOnPause tag
    public void HidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        //Debug.Log("hiding cursor");
        Cursor.visible = true;
    }
}
