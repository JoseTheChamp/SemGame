using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numberOfBoxes = 10;
    private int numberOfBoxesToDeliver;
    private Spawner spawner;
    [HideInInspector]public int level = 1;
    private int maxLevel = 5;
    public float interval = 1f;

    public int Level { get; set; }

    [SerializeField] private GameObject nextLevel;
    [SerializeField] private GameObject lastLevel;
    // Start is called before the first frame update


    void Start()
    {
        numberOfBoxesToDeliver = numberOfBoxes;
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        numberOfBoxesToDeliver = numberOfBoxes;
    }

    private void endgame(){
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

}
