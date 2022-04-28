using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI GameLevelText;
    [SerializeField] private TextMeshProUGUI GameTimeText;
    [SerializeField] private TextMeshProUGUI HighScoreText;
    private int level;

    void Start()
    {
        level = PlayerPrefs.GetInt("GameLevel",0);
        if (level == 0 || level > 4)
        {
            GameLevelText.text = "None";
            GameTimeText.text = "00:00,00";
        }else
        {
            GameLevelText.text = level.ToString();
            GameTimeText.text = formatTime(PlayerPrefs.GetFloat("GameTime",0f));
        }
        HighScoreText.text = formatTime(PlayerPrefs.GetFloat("HighScore",0f));
        if(level > 4){
            PlayerPrefs.SetInt("GameLevel", 0);
            PlayerPrefs.SetFloat("GameTime", 0);
        }
    }

    public void NewGame(){
        PlayerPrefs.SetInt("GameLevel",0);
        PlayerPrefs.SetFloat("GameTime",0f);
        SceneLoader.LoadScene(1);
    }

    public void ContinueGame(){
        SceneLoader.LoadScene(level+1);
    }

    public void Quit(){
        Application.Quit();
    }

    private string formatTime(float time){
        time = time/100;
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 100;
        fraction = (fraction % 100);
        return System.String.Format ("{0:00}:{1:00},{2:00}", minutes, seconds, fraction);
    }
}
