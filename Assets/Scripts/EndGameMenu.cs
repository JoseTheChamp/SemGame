using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mText;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        int level = PlayerPrefs.GetInt("GameLevel",0);
        mText.SetText("LEVEL " + level);
    }

    public void NextLevel(){
        int level = PlayerPrefs.GetInt("GameLevel",0);
        SceneLoader.LoadScene(level+1);
        Debug.Log("NEXT LEVEL");
    }

    public void Exit(){
        SceneLoader.LoadScene("MainMenu");
        Debug.Log("MAIN MENU");
    }

}
