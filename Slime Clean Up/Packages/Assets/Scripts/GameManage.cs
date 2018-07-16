using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour {
    public int currentSeconds = 180;

    private Canvas UI;
    private Text countDownText;
    private GameObject endUI;
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        UI = this.GetComponentInChildren<Canvas>();
        countDownText = UI.GetComponentInChildren<Text>();
        endUI = GameObject.Find("Menu UI");
        endUI.SetActive(false);
        CountDown();
    }

    void CountDown(){
        StartCoroutine(CountDownCo());
    }

    void EndGame(){
        countDownText.enabled = false;
        //GameObject.Find("Players").SetActive(false);
        endUI.SetActive(true);
        //Time.timeScale = 0;
    }
    IEnumerator CountDownCo(){
        while (currentSeconds > 0){
            currentSeconds -= 1;

            if(currentSeconds < 6){
                countDownText.fontSize = Mathf.CeilToInt(countDownText.fontSize * 1.25f);
                countDownText.color = Color.red;
            }
            countDownText.text = currentSeconds.ToString();
            yield return new WaitForSeconds(1f);
        }
        EndGame();
    }


}