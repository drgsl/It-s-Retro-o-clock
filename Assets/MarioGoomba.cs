using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioGoomba : MonoBehaviour
{
    Rigidbody2D rb;

    int dir = 1; // starting right

    int speed = 4;

    bool dead = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            rb.velocity = new Vector2(speed * dir, 0);

            transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right * dir, 1.5f);

            Debug.DrawRay(transform.position, Vector3.right * dir);

            if (hit)
            {
                if (!hit.transform.CompareTag("Mario/Player"))
                {
                    dir = -dir;
                }
            }
        }
    }

    public void Die()
    {
            rb.freezeRotation = false;
            gameObject.GetComponents<BoxCollider2D>()[0].enabled = false;
            gameObject.GetComponents<BoxCollider2D>()[1].enabled = false;
            transform.localScale = new Vector3(transform.localScale.x, 0.3f, transform.localScale.z);
            rb.gravityScale = 1;
            dead = true;
    }
}
