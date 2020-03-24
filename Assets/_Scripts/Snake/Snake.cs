using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq; // because of the list

public class Snake : MonoBehaviour
{

    Vector2 dir = Vector2.right;

    List<Transform> tail = new List<Transform>();

    bool ate = false;

    public GameObject tailPrefab;

    float speed = 1f;

    public SnakeSpawnFood spawner;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.08f, 0.08f);
    }


    private void Update()
    {

        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        if (horiz != 0 || vert !=0)
        {
            dir = new Vector2(horiz, vert);
        }
    }
    void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir * speed);

        // Ate something? Then insert new Element into gap
        if (ate)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            if (Time.timeScale <= 5)
                Time.timeScale += 0.05f;

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.name.StartsWith("food"))
        {
            // Get longer in next Move call
            ate = true;
            spawner.SpawnFood();
            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else
        {
            Debug.Break();
            // ToDo 'You lose' screen
        }
    }
}
