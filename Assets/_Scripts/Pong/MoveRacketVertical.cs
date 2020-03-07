using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRacketVertical : MonoBehaviour
{
    Rigidbody2D rb;

    float speed = 15f;

    public string axis = "Vertical";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxisRaw(axis);

        

        rb.velocity = new Vector2(0, v) * speed;
    }
}
