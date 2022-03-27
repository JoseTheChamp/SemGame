using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    private int numberToSpawn;
    private float interval;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private PhysicsMaterial2D bouncy;
    [SerializeField] private PhysicsMaterial2D unBouncy;
    [SerializeField] private bool isFreeFloating = false;
    [SerializeField] private Sprite circle;
    [SerializeField] private Sprite box;
    private bool isSpawning = false;

    System.Random rnd = new System.Random();

    private GameObject toSpawn;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        numberToSpawn = gameManager.numberOfBoxes;
        interval = gameManager.interval;
        StartCoroutine(Tick());
    }
    IEnumerator Tick()
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

    public void OneMoreBox(){
        numberToSpawn++;
        if (!isSpawning)
        {
            StartCoroutine(Tick());
        }
    }
    private void generateNewBox(){ //TODO randomly generate values
        toSpawn = Instantiate(boxPrefab,this.transform.position,Quaternion.identity);
        toSpawn.SetActive(false);
        Rigidbody2D rigidbody2D= toSpawn.GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = toSpawn.GetComponentInChildren<SpriteRenderer>();
        BoxLogic boxLogic = toSpawn.GetComponent<BoxLogic>();
        //Size
        Random.InitState(System.DateTime.Now.Millisecond);
        //FreeFloating
        if (isFreeFloating)
        {
        rigidbody2D.drag = 0; 
        }
        //Bouncy
        Random.InitState(System.DateTime.Now.Millisecond);   
        if (rnd.Next(0,4) < 3) //TODO different graphic if bouncy/unbouncy
        {
            rigidbody2D.sharedMaterial = bouncy;
        }else{
            rigidbody2D.sharedMaterial = unBouncy;
        }
        Random.InitState(System.DateTime.Now.Millisecond);        //Shape
        if (rnd.Next(0,3) < 2)
        {
            spriteRenderer.sprite = circle;
            toSpawn.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }else{
            spriteRenderer.sprite = box;
            toSpawn.GetComponentInChildren<CircleCollider2D>().enabled = false;
        }
        Random.InitState(System.DateTime.Now.Millisecond);        //Color
        int i = rnd.Next(0,3);
        if (i == 0)
        {
            boxLogic.color = ColorType.blue;
            spriteRenderer.color = Color.blue;
        }else if(i==1){
            boxLogic.color = ColorType.green;
            spriteRenderer.color = Color.green;
        }else{
            boxLogic.color = ColorType.red;
            spriteRenderer.color = Color.red;
        }
        toSpawn.SetActive(true);
        rigidbody2D.AddForce(new Vector2(Random.Range(-50f,50f),Random.Range(-50f,50f)));
    }
}
