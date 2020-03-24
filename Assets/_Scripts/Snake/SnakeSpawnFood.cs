using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawnFood : MonoBehaviour
{
    public GameObject food;

    public Transform borders;
    Vector2 maxs;
    Vector2 mins;

    float foodSpawnRate = .1f;

    float scale = 1f;

    void Start()
    {
        for (int i = 0; i < borders.childCount; i++)
        {
            Vector3 child = borders.GetChild(i).transform.position;
            float childX = child.x;
            float childY = child.y;
            
            if (childX > maxs.x)
                maxs.x = childX;
            if (childX < mins.x)
                mins.x = childX;

            if (childY > maxs.y)
                maxs.y = childY;
            if (childY < mins.y)
                mins.y = childY;
        }
        SpawnFood();
        //InvokeRepeating("SpawnFood", foodSpawnRate, foodSpawnRate);
    }

    public void SpawnFood()
    {
        // x position between left & right border
        int x = (int)Random.Range(mins.x, maxs.x);

        // y position between top & bottom border
        int y = (int)Random.Range(mins.y, maxs.y);

        // Instantiate the food at (x, y)
        Transform go = Instantiate(food, new Vector2(x, y), Quaternion.identity).transform; // default rotation

        scale += 0.2f;
        go.localScale = new Vector3(scale, scale, 0);

    //Note: x and y are rounded via(int) to make sure that the food is always spawned at a position like(1, 2) but never at something like(1.234, 2.74565).
    } 
}
