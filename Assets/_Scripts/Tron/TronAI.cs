using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TronAI : MonoBehaviour
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

    [Range(0.1f, 2.0f)]
    public float Difficulty = 1f;


    Vector2 bestOption = new Vector2(-1f, -1f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
        spawnWall();

        InvokeRepeating("Move", 0f, Difficulty);
    }

    void Move()
    {
        Vector2 dir;

        float bestDistance = -1f;

        for (int i = -1; i <=1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if ((i == 0 && j == 0) || (i == j) || (i == -1 * j)) // check for movement update and also eliminate diagonal movement
                    continue;
                dir = new Vector2(i, j);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
                Debug.DrawRay(transform.position, dir, Color.green);
                //Debug.Log(hit.distance);
                if (hit.distance > bestDistance)
                {
                    bestOption = dir;
                    bestDistance = hit.distance;
                }
            }
        }
        spawnWall();
    }

    private void Update()
    {
        rb.velocity = bestOption * speed;
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
