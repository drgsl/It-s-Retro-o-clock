using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRacketHorizontal : MonoBehaviour
{

    Rigidbody2D rb;

    float speed = 400f;

    public string axis = "Horizontal";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float h = Input.GetAxis(axis);

        rb.velocity = new Vector2(h, 0) * speed;
    }
}
