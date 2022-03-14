using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    private float swingSpeed;
    private float maxSwingSpeed;
    private bool isThrown = false;

    public bool isInRange = false;

    private Collider2D playerCollider;
    private Collider2D myCollider;
    void Start()
    {
        SwingManager sm = GameObject.Find("GameManager").GetComponent<SwingManager>();
        swingSpeed = sm.swingSpeed;
        maxSwingSpeed = sm.maxSwingSpeed;
        playerCollider = GameObject.Find("Player").GetComponent<Collider2D>();
        myCollider = this.gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isThrown){
            Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
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
            isThrown=false;
            Physics2D.IgnoreCollision(playerCollider,myCollider,false);

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
