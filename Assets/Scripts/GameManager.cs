using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int numberOfBoxes = 10;
    [SerializeField] TextMeshProUGUI text;
    private int numberOfBoxesToDeliver;
    private Spawner spawner;
    [HideInInspector]public int level = 1;
    private int maxLevel = 5;
    public float interval = 1f;
    private static float timer;
    private bool gameRunning = true;

    public int Level { get; set; }

    [SerializeField] private GameObject nextLevel;
    [SerializeField] private GameObject lastLevel;
    // Start is called before the first frame update


    void Start()
    {
        numberOfBoxesToDeliver = numberOfBoxes;
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        numberOfBoxesToDeliver = numberOfBoxes;
        timer = 0;
    }

    private void Update()
    {
        if (gameRunning)
        {
            timer += Time.deltaTime;
            FormatToTime();   
        }
    }

    private void FormatToTime(){
        /*
        float minutes = Mathf.Round(timer / 60);
        float seconds = Mathf.RoundToInt(timer%60);
        string minutesText,secondsText;
        minutesText = minutes.ToString();
        if(minutes < 10) {
            minutesText = "0" + minutes.ToString();
        }
        secondsText = seconds.ToString();
        if(seconds < 10) {
            secondsText = "0" + Mathf.RoundToInt(seconds).ToString();
        }
        text.text = minutesText + ":" + secondsText;
        */
        int intTime = (int)timer;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = timer * 100;
        fraction = (fraction % 100);
        string timeText = System.String.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        text.text = timeText;
    }

    private void endgame(){
        GameObject.Find("Enemy").GetComponent<Enemy>().enabled = false;
        gameRunning = false;
        Debug.Log("KONEC HRY CAS: " + timer);
        if (level < maxLevel)
        {
            Debug.Log("NEXT");
            nextLevel.SetActive(true);
        }else{
            Debug.Log("LAST");
            lastLevel.SetActive(true);
        }
    }

    public void BoxDelivered(){
        numberOfBoxesToDeliver--;
        Debug.Log("BOX delivered");
        if (numberOfBoxesToDeliver == 0)
        {
            Debug.Log("ending game");
            endgame();
        }
    }
    public void RestoreBox(){
        spawner.OneMoreBox();
    }

    public void addTime(float time){
        timer += time;
    }

}
