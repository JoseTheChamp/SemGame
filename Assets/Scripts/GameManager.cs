using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int numberOfBoxes = 10;
    [SerializeField] TextMeshProUGUI textTime;
    private int numberOfBoxesToDeliver;
    private Spawner spawner;
    [HideInInspector]public int level = 0;
    private int maxLevel = 5;
    public float interval = 1f;
    private static float timer;
    private bool gameRunning = true;

    public int Level { get; set; }

    [SerializeField] private GameObject nextLevel;
    [SerializeField] private GameObject lastLevel;

    void Start()
    {
        //-----------------
        Debug.Log("Hotove levely: " + PlayerPrefs.GetInt("GameLevel",0));
        //----------------
        level = PlayerPrefs.GetInt("GameLevel",0);
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
        int intTime = (int)timer;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = timer * 100;
        fraction = (fraction % 100);
        string timeText = System.String.Format ("{0:00}:{1:00},{2:00}", minutes, seconds, fraction);
        textTime.text = timeText;
    }

    private void endgame(){
        GameObject go = GameObject.Find("Enemy");
        if (go != null)
        {
            go.GetComponent<Enemy>().enabled = false;
        }
        gameRunning = false;
        PlayerPrefs.SetInt("GameLevel",level+1);
        Debug.Log("KONEC HRY CAS: " + timer);
        updateGameTime();
        if (level < maxLevel)
        {
            Debug.Log("NEXT");
            nextLevel.SetActive(true);
        }else{
            Debug.Log("LAST");
            updateHighScore();
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

    private void updateGameTime(){
        float gameTime = PlayerPrefs.GetFloat("GameTime",0);
        gameTime += Mathf.Round(timer*100);
        PlayerPrefs.SetFloat("GameTime",gameTime);
    }

    private void updateHighScore(){
        float highScore = PlayerPrefs.GetFloat("HighScore",0f);
        float gameTime = PlayerPrefs.GetFloat("GameTime",0f);
        if (gameTime < highScore)
        {
            Debug.Log("NOVE HIGHSCORE");
            PlayerPrefs.SetFloat("HighScore",gameTime);
        }
    }
}
