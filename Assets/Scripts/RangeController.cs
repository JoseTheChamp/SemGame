using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeController : MonoBehaviour
{
    private BoxMovement boxMovement;
    private void Start()
    {
        boxMovement = this.gameObject.GetComponentInParent<BoxMovement>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            boxMovement.isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            boxMovement.isInRange = false;
        }
    }
}
