using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour //řeší pohyb boxu, kdy se hráč smí dotknout, kdy se spojení přeruší atd.
{
    private Vector2 oldPosition;
    [SerializeField] private float range = 3.6f;
    private bool isThrown = false;
    private bool isInRange = false;
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private Collider2D myCollider;
    [SerializeField] private AudioSource bounceAudio;
    [SerializeField] private float swingSpeed = 300;
    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<CircleCollider2D>();
        myCollider = this.GetComponentInChildren<BoxCollider2D>();
        if (!myCollider.isActiveAndEnabled)
        {
            myCollider = this.GetComponentInChildren<CircleCollider2D>();
        }

        rb = this.gameObject.GetComponent<Rigidbody2D>();
        oldPosition = this.transform.position;
    }

    void FixedUpdate()
    {
        if(isThrown){ //pokud právě házím - vypočítá a přidá se síla
            Vector2 speed = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            speed = speed - (Vector2)this.gameObject.transform.position;
            speed = speed * swingSpeed * Time.deltaTime;
            rb.AddForce(speed);
        }
        if (!isInRange)
        {
            isThrown = false;
        }
    }

    private void Update()
    {
        if(Vector2.Distance(playerCollider.transform.position,this.transform.position)<range){ // kontrola jestli je hráč v dosahu
            isInRange = true;
        }else{
            isInRange = false;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isInRange) //zahájení házení
            {
                isThrown = true;
                Physics2D.IgnoreCollision(playerCollider,myCollider); 
            }
        }
    }

    private void OnMouseUp() //zkončení hodu hráčem
    {
        isThrown = false;
        Physics2D.IgnoreCollision(playerCollider,myCollider,false);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        bounceAudio.Play();
    }
}
