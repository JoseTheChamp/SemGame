using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dock : MonoBehaviour //Místo pro doručení boxu - podle určené barvy skontroluje správnost boxu a provede určenou akci.
{
[SerializeField] ColorType color;
[SerializeField] Image image;
[SerializeField] AudioSource DeliverAudioFailed;
[SerializeField] AudioSource DeliverAudioSucces;
private GameManager gameManager;
private bool isBlocked = false;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        switch (color) //Nastavení barvy spawneru
        {
            case ColorType.blue:
                image.color = new Color(0,0,1,0.4f);
            break;
            case ColorType.green:
                image.color = new Color(0,1,0,0.4f);
            break;
            case ColorType.red:
                image.color = new Color(1,0,0,0.4f);
            break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            if (!isBlocked)
            {
                if (other.GetComponentInParent<BoxLogic>().Color == this.color) 
                { //Doručení
                    DeliverAudioSucces.Play();
                    Destroy(other.transform.parent.gameObject);
                    gameManager.BoxDelivered();
                }else{ //resetovaní boxu
                    DeliverAudioFailed.Play();
                    Destroy(other.transform.parent.gameObject);
                    gameManager.RestoreBox();
                    gameManager.addTime(5);
                } 
            }
        }
        if (other.tag == "Enemy") //pokud enemy se nachází v oblasti spawneru spawner nebude schopen přijímat boxy.
        {
            isBlocked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            isBlocked = false;
        }
    }
}
