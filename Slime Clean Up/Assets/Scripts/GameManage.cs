using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour {
    public int currentSeconds = 60;
    public static int winAmount = 250;
    public static bool endBool = false;

    private Canvas UI;
    private Text countDownText;
    private GameObject endUI;
    private Slider progressBar;
    [SerializeField]
    private float fill = 0;
    [SerializeField]
    private int UpdateTime = 0;
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        UI = this.GetComponentInChildren<Canvas>();
        countDownText = UI.GetComponentInChildren<Text>();
        progressBar = UI.GetComponentInChildren<Slider>();
        endUI = Resources.FindObjectsOfTypeAll<EndMenu>()[0].gameObject;
        endUI.SetActive(false);

        CountDown();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (UpdateTime % 60 == 0)
        {
            fill = 0;
            foreach(GameObject g in GameObject.FindGameObjectsWithTag("Brush"))
            {
                fill += g.GetComponent<DeleteBrush>().val;
            }
            progressBar.value = fill / winAmount;
        } 
        else
        {
            UpdateTime +=1 ;
        }
    }

    void CountDown()
    {
        StartCoroutine(CountDownCo());
    }

    void EndGame()
    {
        countDownText.enabled = false;
        endUI.SetActive(true);
        endBool = true;
        Cursor.visible = true;
    }

    IEnumerator CountDownCo()
    {
        while (currentSeconds > 0)
        {
            currentSeconds -= 1;

            if(currentSeconds < 6)
            {
                countDownText.fontSize = Mathf.CeilToInt(countDownText.fontSize * 1.25f);
                countDownText.color = Color.red;
            }
            countDownText.text = currentSeconds.ToString();
            yield return new WaitForSeconds(1f);
        }
        EndGame();
    }
}