using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dock : MonoBehaviour
{
[SerializeField] private ColorType color;
private GameManager gameManager;
[SerializeField] private Image image;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            if (other.GetComponentInParent<BoxLogic>().color == this.color)
            {
                Destroy(other.transform.parent.gameObject);
                gameManager.BoxDelivered();
                //TakeOut make contact to gameManager
            }else{
                Destroy(other.transform.parent.gameObject);
                gameManager.RestoreBox();
            } //else take out a poslat zpatky na spawner ???? asi jo ale jednoduse neboli to spawn +1
        }
    }
}
