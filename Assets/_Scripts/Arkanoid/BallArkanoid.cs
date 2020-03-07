using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArkanoid : MonoBehaviour
{

    public static float speed = 150f;

    Rigidbody2D rb;
    public GameObject racket;

    const float scaleIncrement = 0.005f;
    Vector3 increment = new Vector3(scaleIncrement, scaleIncrement, 0);
    //float defaultScale = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        restartVelocity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.name == "Racket")
        {
            float x = hitFactor(transform.position,
                  collision.transform.position,
                  collision.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            rb.velocity = dir * speed;

        }

        if (collision.gameObject.GetComponent<BrickArkanoid>())
        {
            racket.transform.localScale -= increment;
            transform.localScale += increment;
            if (racket.transform.localScale.x <= 0.5f)
            {
                racket.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            }
            ArkanoidScore.newScore += ArkanoidScore.brickScore;

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "UnderLevel")
        {
            ArkanoidScore.newScore -= ArkanoidScore.underLevelScore;
            if (ArkanoidScore.newScore <= 0)
            {
                ArkanoidScore.newScore = 0;
            }
            restartVelocity();
            //racket.transform.localScale = new Vector3(defaultScale, defaultScale, 1);
        }

    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketWidth)
    {
        // ascii art:
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        //
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    public void restartVelocity()
    {
        transform.position = racket.transform.position + new Vector3(0, 15, 0);
        rb.velocity = new Vector2(Random.Range(-1f, 1f), 1) * speed;
    }
}
