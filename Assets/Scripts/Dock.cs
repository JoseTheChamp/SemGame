using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dock : MonoBehaviour
{
[SerializeField] private ColorType color;
private GameManager gameManager;
[SerializeField] private Image image;
private bool isBlocked = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        switch (color)
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
                if (other.GetComponentInParent<BoxLogic>().color == this.color)
                {
                    Destroy(other.transform.parent.gameObject);
                    gameManager.BoxDelivered();
                    //TakeOut make contact to gameManager
                }else{
                    //TODO add time to clock
                    Destroy(other.transform.parent.gameObject);
                    gameManager.RestoreBox();
                    gameManager.addTime(5);
                } //else take out a poslat zpatky na spawner ???? asi jo ale jednoduse neboli to spawn +1   
            }
        }
        if (other.tag == "Enemy")
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
