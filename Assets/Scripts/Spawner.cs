using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    private int numberToSpawn;
    private float interval;
    private bool isRugged = false;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private PhysicsMaterial2D bouncy;
    [SerializeField] private PhysicsMaterial2D unBouncy;
    [SerializeField] private bool isFreeFloating = false;
    [SerializeField] private Sprite circle;
    [SerializeField] private Sprite box;
    [SerializeField] private Sprite boxRugged;
    [SerializeField] private Sprite circleRugged;


    private bool isSpawning = false;
    System.Random rnd = new System.Random();
    private GameObject toSpawn;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        numberToSpawn = gameManager.NumberOfBoxes;
        interval = gameManager.Interval;
        StartCoroutine(Tick());
    }
    IEnumerator Tick() // proces spawnování
    {
        isSpawning = true;
        yield return new WaitForSeconds(interval);
        if (numberToSpawn>0)
        {
            numberToSpawn--;
            generateNewBox();
            StartCoroutine(Tick());
        }  
        isSpawning = false;      
    }

    public void OneMoreBox(){ //spawne box navíc - byl doručen špatně
        numberToSpawn++;
        if (!isSpawning)
        {
            StartCoroutine(Tick());
        }
    }
    private void generateNewBox(){ // proces vytvoření nového boxu
        toSpawn = Instantiate(boxPrefab,this.transform.position,Quaternion.identity);
        toSpawn.SetActive(false);
        Rigidbody2D rigidbody2D= toSpawn.GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = toSpawn.GetComponentInChildren<SpriteRenderer>();
        BoxLogic boxLogic = toSpawn.GetComponent<BoxLogic>();
        Random.InitState(System.DateTime.Now.Millisecond);

        //FreeFloating
        if (isFreeFloating)
        {
        rigidbody2D.drag = 0; 
        }

        //Bouncy
        Random.InitState(System.DateTime.Now.Millisecond);   
        if (rnd.Next(0,4) < 3)
        {
            rigidbody2D.sharedMaterial = bouncy;
            isRugged = false;
        }else{
            rigidbody2D.sharedMaterial = unBouncy;
            isRugged = true;
        }

        //shape
        Random.InitState(System.DateTime.Now.Millisecond);
        if (rnd.Next(0,3) < 2)
        {
            if (isRugged)
            {
                spriteRenderer.sprite = circleRugged;
            }else{
                spriteRenderer.sprite = circle;
            }
            toSpawn.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }else{
            if (isRugged)
            {
                spriteRenderer.sprite = boxRugged;
            }else{
                spriteRenderer.sprite = box;
            }
            toSpawn.GetComponentInChildren<CircleCollider2D>().enabled = false;
        }

        //Color
        Random.InitState(System.DateTime.Now.Millisecond);
        int i = rnd.Next(0,3);
        if (i == 0)
        {
            boxLogic.Color = ColorType.blue;
            spriteRenderer.color = Color.blue;
        }else if(i==1){
            boxLogic.Color = ColorType.green;
            spriteRenderer.color = Color.green;
        }else{
            boxLogic.Color = ColorType.red;
            spriteRenderer.color = Color.red;
        }


        toSpawn.SetActive(true); //spawnutí
        rigidbody2D.AddForce(new Vector2(Random.Range(-50f,50f),Random.Range(-50f,50f))); //přidání síly aby boxy se spawnovali správně
    }
}
