using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float intervalBehaviour = 7f;
    [SerializeField] private float startInterval = 4f;
    [SerializeField] private float spawnInterval = 4f;
    [SerializeField] private float dockInterval = 8f;
    [SerializeField] private float speed = 6.5f;
    [SerializeField] private Image healthImage; 
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] AudioSource playerHit;
    private float plusInterval = 0;
    private Transform playerTransform;
    private Vector2 startingPosition;
    private Transform nearestDock;
    private Transform nearestBox;
    private Vector2 target;
    private Vector2 targetDirection;
    private bool respawning = false;
    private bool revive = false;
    private Rigidbody2D rb;
    private int state = 0;
    private bool evenState = true;
    private System.Random rnd = new System.Random();
    private CircleCollider2D myColider;
    private SpriteRenderer mySpriteRenderer;
    private GameManager gameManager;
    private float health;

    void Start()
    {
        health = maxHealth;
        playerTransform = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startingPosition = this.transform.position;
        myColider = GetComponent<CircleCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("ChangeBehaviour");
    }

    IEnumerator ChangeBehaviour() // tato metoda se zavolá po startInterval a pote vždy po proměnlivé položce
    {
        yield return new WaitForSeconds(startInterval); 
        while (true) // hlavní cyklus chování NPC
        {
            if (revive)
            {
                revive = false;
                healthImage.transform.parent.gameObject.SetActive(true);
                this.transform.position = startingPosition;
                myColider.enabled = true;
                mySpriteRenderer.enabled = true;
                respawning = false;
                evenState = true;
                health = maxHealth;
            }
            plusInterval = 0;
            if(respawning){
                revive = true;
                plusInterval += spawnInterval;
            }else{
                if (evenState)
                {
                    evenState = false;
                    state = 1; // pohyb směrem k hráči - zranit hráče - přidat mu čas
                }else{
                    evenState = true;
                    state = rnd.Next(2,4);
                    if (state == 2)
                    {//pohyb směrem k doku - blokovat dock
                        GameObject[] docks = GameObject.FindGameObjectsWithTag("Dock");
                        if (docks != null)
                        {
                            float smallestDistance = Vector2.Distance(docks[0].transform.position,transform.position);
                            int smallestIndex = 0;
                            for (int i = 1; i < docks.Length; i++)
                            {
                                float distance = Vector2.Distance(docks[i].transform.position,transform.position);
                                if(distance<smallestDistance){
                                    smallestDistance = distance;
                                    smallestIndex = i;
                                }
                            }
                            nearestDock = docks[smallestIndex].transform;
                            plusInterval += dockInterval;
                        }

                    }else{//pohyb směrem k boxu - zničit box
                        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Item");
                        if (boxes != null)
                        {
                            float smallestDistance = Vector2.Distance(boxes[0].transform.position,transform.position);
                            int smallestIndex = 0;
                            for (int i = 1; i < boxes.Length; i++)
                            {
                                float distance = Vector2.Distance(boxes[i].transform.position,transform.position);
                                if(distance<smallestDistance){
                                    smallestDistance = distance;
                                    smallestIndex = i;
                                }
                            }
                            nearestBox = boxes[smallestIndex].transform;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(intervalBehaviour + plusInterval);  
        }
    }

    private void Update() //určení cíle a vypočtení směru
    {
        if (!respawning)
        {
            switch (state)
            {
                case 1: // head toward player
                target = playerTransform.position;
                break;
                case 2: // head towards dock
                target = nearestDock.position;
                break;
                case 3: // head towards box
                target = nearestBox.position;
                break;
            }
            targetDirection = new Vector2(target.x - transform.position.x,target.y - transform.position.y); 
            healthImage.fillAmount = health/maxHealth;
        }
    }

    private void FixedUpdate() //přidání síli směrem k hráči
    {
        if (!respawning)
        {
            rb.AddForce(targetDirection.normalized * speed); 
        }

    }

    private void Respawn(){ // Enemy byl zničen/se zničil
        respawning = true;
        myColider.enabled = false;
        mySpriteRenderer.enabled = false;
        healthImage.transform.parent.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("ENEMY COLLISION");
        if (other.gameObject.tag == "Item") // pokud na razil na box - znič se, znič box
        {
            Debug.Log("ENEMY COLLISION - item");
            if (GameObject.ReferenceEquals(other?.gameObject,nearestBox?.transform.parent.gameObject))
            {
                Destroy(other.gameObject);
                gameManager.RestoreBox();
                Respawn();
            }else{
                Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
                float damage = (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y)) * rb.mass * 5;
                if (damage > 8)
                {
                    health -= damage;
                    if(health < 0.5f){
                        health = 0;
                        Respawn();
                    }   
                }
            }
        }
        if (other.gameObject.tag == "Player") //pokud narazil na hráče - znič se a přidej čas.
        {
            Debug.Log("ENEMY COLLISION - player");
            playerHit.Play();
            gameManager.addTime(10);
            Respawn();
        }
    }
}