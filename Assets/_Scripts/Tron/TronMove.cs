using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TronMove : MonoBehaviour
{
    // Movement Speed
    float speed = 16;

    // Wall Prefab
    public GameObject wallPrefab;

    // Current Wall
    Collider2D wall;

    // Last Wall's End
    Vector2 lastWallEnd;

    Rigidbody2D rb;

    void Start()
    {
        // Initial Velocity
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * speed;
        spawnWall();
    }

    void FixedUpdate()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        if ((horiz != 0 || vert != 0) && (horiz != vert) && (horiz != -1 * vert)) // check for movement update and also eliminate diagonal movement
        {
            //Debug.Log( "horiz : " +  horiz);
            //Debug.Log("verti : " + vert);
            rb.velocity = new Vector2(horiz, vert) * speed;
            spawnWall();
        }

        fitColliderBetween(wall, lastWallEnd, transform.position);


    }

    void spawnWall()
    {
        // Save last wall's position
        lastWallEnd = transform.position;

        // Spawn a new Lightwall
        GameObject g = (GameObject)Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
    }


    void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        // Calculate the Center Position
        co.transform.position = a + (b - a) * 0.5f;

        // Scale it (horizontally or vertically)
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
            co.transform.localScale = new Vector2(dist + 1, 1);
        else
            co.transform.localScale = new Vector2(1, dist + 1);
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        // Not the current wall?
        if (co != wall)
        {
            print("Player lost:" + name + "Due to: " + co.gameObject);
            Debug.Break();
            //Destroy(gameObject);
        }
    }
}
