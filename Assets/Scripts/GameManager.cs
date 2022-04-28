using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int numberOfBoxes = 6;
    [SerializeField] private TextMeshProUGUI textTime;
    private int numberOfBoxesToDeliver;
    private Spawner spawner;
    private int level = 0;
    private int maxLevel = 5;
    public float Interval {get; set;} = 1f;
    private static float timer;
    private bool gameRunning = true;
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private GameObject lastLevel;

    void Start()
    {
        level = PlayerPrefs.GetInt("GameLevel",0);
        numberOfBoxesToDeliver = numberOfBoxes;
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        numberOfBoxesToDeliver = numberOfBoxes;
        timer = 0;
    }

    private void Update() //běh časomíry
    {
        if (gameRunning)
        {
            timer += Time.deltaTime;
            FormatToTime();   
        }
    }

    private void FormatToTime(){ //formátování milisekund do zobrazovaného textu
        int intTime = (int)timer;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = timer * 100;
        fraction = (fraction % 100);
        string timeText = System.String.Format ("{0:00}:{1:00},{2:00}", minutes, seconds, fraction);
        textTime.text = timeText;
    }

    private void endgame(){ //ukončování hry - vyppnutí hráče,posunutí levelu
        GameObject go = GameObject.Find("Enemy");
        if (go != null)
        {
            go.GetComponent<Enemy>().enabled = false;
        }
        gameRunning = false;
        PlayerPrefs.SetInt("GameLevel",level+1);
        Debug.Log("KONEC HRY CAS: " + timer);
        updateGameTime();
        if (level+1 != maxLevel)
        {
            nextLevel.SetActive(true);
        }else{
            updateHighScore();
            lastLevel.SetActive(true);
        }
    }

    public void BoxDelivered(){ //box dosáhl svého cíle.
        numberOfBoxesToDeliver--;
        if (numberOfBoxesToDeliver == 0)
        {
            endgame();
        }
    }
    public void RestoreBox(){ //box nedosáhl cíle takže je potřeba spwanout jeden box navíc
        spawner.OneMoreBox();
    }

    public void addTime(float time){ //přidání trestného času
        timer += time;
    }

    private void updateGameTime(){ //update zobrazovaného času
        float gameTime = PlayerPrefs.GetFloat("GameTime",0);
        gameTime += Mathf.Round(timer*100);
        PlayerPrefs.SetFloat("GameTime",gameTime);
    }

    private void updateHighScore(){ // update highscore na konci hry - porovnání s pamětí
        float highScore = PlayerPrefs.GetFloat("HighScore",0f);
        float gameTime = PlayerPrefs.GetFloat("GameTime",0f);
        Debug.Log("UpdateHighScore: " + gameTime + " < " + highScore + "???");
        if (gameTime < highScore || highScore == 0)
        {
            Debug.Log("NOVE HIGHSCORE");
            PlayerPrefs.SetFloat("HighScore",gameTime);
        }
    }
}
