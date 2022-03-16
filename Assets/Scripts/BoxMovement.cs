using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    private float swingSpeed;
    private float maxSwingSpeed;
    [SerializeField] float range = 3.6f;
    [HideInInspector]public bool isThrown = false;

    [HideInInspector]public bool isInRange = false;
    
    private Rigidbody2D rb;
    Collider2D playerCollider;
    Collider2D myCollider;
    void Start()
    {
        SwingManager sm = GameObject.Find("GameManager").GetComponent<SwingManager>();
        swingSpeed = sm.swingSpeed;
        maxSwingSpeed = sm.maxSwingSpeed;
        playerCollider = GameObject.Find("Player").GetComponent<CircleCollider2D>();
        myCollider = this.GetComponentInChildren<BoxCollider2D>();
        if (!myCollider.isActiveAndEnabled)
        {
            myCollider = this.GetComponentInChildren<CircleCollider2D>();
        }

        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isThrown){
            Vector2 speed = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            speed = speed - (Vector2)this.gameObject.transform.position;
            speed = speed * swingSpeed * Time.deltaTime;
            if(speed.x > maxSwingSpeed || speed.y > maxSwingSpeed){
                Debug.Log("MAXING OUT - " + speed + "   max: " + maxSwingSpeed);
            }
            rb.AddForce(speed);
        }
        if (!isInRange)
        {
            isThrown = false;
        }
    }

    private void Update()
    {
        //Debug.Log(Vector2.Distance(playerCollider.transform.position,this.transform.position));
        if(Vector2.Distance(playerCollider.transform.position,this.transform.position)<range){
            isInRange = true;
        }else{
            isInRange = false;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isInRange)
            {
            isThrown = true;
            Physics2D.IgnoreCollision(playerCollider,myCollider); 
            }
        }
    }

    private void OnMouseUp()
    {
        isThrown = false;
        Physics2D.IgnoreCollision(playerCollider,myCollider,false);
    }
}
