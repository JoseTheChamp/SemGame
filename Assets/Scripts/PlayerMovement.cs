using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 400f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        moveSpeed *= rb.mass;
    }

    // Update is called once per frame
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
