using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour //Spravuje boxům a hráči drag v oblasti tohoto objektu
{

    [SerializeField] private float Drag { get; set; } = 4.5f;

    private class ItemDrag{ // zachovavá informace o hodnotě drag u objektu
        public int instatnceID;
        public float drag;

        public ItemDrag(float drag, int instatnceID)
        {
            this.instatnceID = instatnceID;
            this.drag = drag;
        }
    }
    [SerializeField] bool isSlippery;

    List<ItemDrag> list = new List<ItemDrag>();

    private void OnTriggerEnter2D(Collider2D other) // zmení drag a zapíše hodnotu do itemDrag
    {
        if (other.gameObject.tag == "Item")
        {
            Rigidbody2D rb = other.GetComponentInParent<Rigidbody2D>();
            list.Add(new ItemDrag(rb.drag,other.gameObject.GetInstanceID())); 
            if (isSlippery)
            {
                rb.drag = 0;
            }else{
                rb.drag = Drag;
            }
        }
        if (other.gameObject.tag == "Player")
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            list.Add(new ItemDrag(rb.drag,other.gameObject.GetInstanceID())); 
            if (isSlippery)
            {
                rb.drag = 0;
            }else{
                rb.drag = Drag;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) // vrati hodnotu zpět z itemDrag
    {
        if (other.gameObject.tag == "Item")
        {
            ItemDrag itemDrag = list.Find(x => x.instatnceID == other.gameObject.GetInstanceID());
            Rigidbody2D rb = other.GetComponentInParent<Rigidbody2D>();
            rb.drag = itemDrag.drag;
        }
        if (other.gameObject.tag == "Player")
        {
            ItemDrag itemDrag = list.Find(x => x.instatnceID == other.gameObject.GetInstanceID());
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.drag = itemDrag.drag;
            list.Remove(itemDrag);
        }
    }
}
