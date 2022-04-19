using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour //správa pohybu hráče.
{
    [SerializeField] private float moveSpeed = 400f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        moveSpeed *= rb.mass;
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float forceX = x * moveSpeed * Time.deltaTime;
        float forceY = y * moveSpeed * Time.deltaTime;

        Vector2 force = new Vector2(forceX,forceY);

        rb.AddForce(force);
    }
}
