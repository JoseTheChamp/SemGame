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
        mText.SetText("LEVEL " + gameManager.level);
    }

    public void NextLevel(){
        //TODO load next scene
        Debug.Log("NEXT LEVEL");
    }

    public void Exit(){
        //TODO get to main menu
        Debug.Log("MAIN MENU");
    }

}
