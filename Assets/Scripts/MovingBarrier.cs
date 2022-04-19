using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBarrier : MonoBehaviour
{
    private Vector2 startPossition;
    [SerializeField] private bool axisIsX;
    [SerializeField] private bool upOrLeft;
    [SerializeField] private float lenght = 3f;
    private int i;
    void Start() //Na základě paramatrů nastaví styl pohybu
    {
        startPossition = this.transform.position;
        i = -1;
        if (upOrLeft)
        {
            i = 1;
        }
    }
    void Update() //spravuje pohyb, pohyblivé překážky
    {
        if (axisIsX)
        {
            transform.position = startPossition + new Vector2((Mathf.Sin(Time.time) + 1) * i * lenght, 0.0f);
        }else{
            transform.position = startPossition + new Vector2(0.0f,(Mathf.Sin(Time.time) + 1) *i* lenght);
        }
    }
}
