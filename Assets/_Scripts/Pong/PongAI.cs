using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongAI : MonoBehaviour
{
    public GameObject ball;
    public Ball ballScript;

    float speed = 9f;
    Vector3 move = Vector3.zero;
    
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballScript = ball.GetComponent<Ball>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float verticalDist = ball.transform.position.y - transform.position.y;
        float horizontalDist = ball.transform.position.x - transform.position.x;
        //Debug.Log(horizontalDist);
        if (horizontalDist > -10)
        {
            if (verticalDist > 0)
            {
                move.y = speed * Mathf.Min(verticalDist, 1.0f);
            }
            if (verticalDist < 0)
            {
                move.y = -(speed * Mathf.Min(-verticalDist, 1.0f));
            }
            rb.velocity = new Vector2(0, move.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == ball)
        {
            ballScript.restartVelocity();
        }
    }
}
