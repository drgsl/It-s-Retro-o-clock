using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    const float MIN_SPEED = 10f;
    float speed = MIN_SPEED;
    float spdIncrement = 1f;
    float scaleIncrement = 0.02f;

    Vector3 RacketStartScale;

    public GameObject LeftRacket;
    public GameObject RightRacket;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        restartVelocity();
        RacketStartScale = LeftRacket.transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Racket Left")
        {
            float y = hitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);

            Vector2 dir = new Vector2(1, y).normalized;

            rb.velocity = dir * speed;

            PongScore.LeftScore += 1;
            speed += spdIncrement;
            collision.gameObject.transform.localScale -= new Vector3(0, scaleIncrement, 0);
        }

        if (collision.gameObject.name == "Racket Right")
        {
            float y = hitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);

            Vector2 dir = new Vector2(-1, y).normalized;

            rb.velocity = dir * speed;

            PongScore.RightScore += 1;
            speed += spdIncrement;
            collision.gameObject.transform.localScale -= new Vector3(0, scaleIncrement, 0);
        }

        if (collision.gameObject.name == "WallLeft")
        {
            PongScore.LeftScore = 0;
            speed = MIN_SPEED;
            LeftRacket.transform.localScale = RacketStartScale;
        }

        if (collision.gameObject.name == "WallRight")
        {
            PongScore.RightScore = 0;
            speed = MIN_SPEED;
            RightRacket.transform.localScale = RacketStartScale;
        }
    }

    private void Update()
    {
        if (transform.position.x < -11f || transform.position.x > 11f ||
            transform.position.y < -6f || transform.position.y > 6f)
        {
            restartVelocity();
        }
        
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        //  1 = at the    top of the racket
        //
        //  0 = at the middle of the racket
        //
        // -1 = at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    public void restartVelocity()
    {
        transform.position = Vector3.zero;
        rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * speed;
    }
}
