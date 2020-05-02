using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerArkanoid : MonoBehaviour
{
    int maxBlocksW = 23;
    int maxBlocksH = 7;
    public GameObject block;
    Transform[] Corners = new Transform[4];
    float blockW = 16f;
    float blockH = 8f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Corners.Length; i++)
        {
            Corners[i] = transform.GetChild(i);
            //Debug.Log(i + Corners[i].transform.name);
        }
        for (int i = 0; i < maxBlocksW; i++)
        {
            for (int j = 0; j < maxBlocksH; j++)
            {
                GameObject go = Instantiate(block, new Vector3(block.transform.position.x + blockW * i, block.transform.position.y - blockH * j, 0), Quaternion.identity);
                go.transform.parent = transform;
                //if (!insideBorder(go.transform.position))
                //{
                //    Debug.Log("Destroyed Block");
                //    Destroy(go);
                //}
            }
        }
        Destroy(block);
    }

    //bool insideBorder(Vector3 pos)
    //{
    //    Vector2 mins = new Vector2(-180, -60);
    //    Vector2 maxs = new Vector2(180, 100);

    //    mins.x = Mathf.Min(Corners[0].position.x, Corners[1].position.x, Corners[2].position.x, Corners[3].position.x);
    //    mins.y = Mathf.Min(Corners[0].position.y, Corners[1].position.y, Corners[2].position.y, Corners[3].position.y);
    //    maxs.x = Mathf.Max(Corners[0].position.x, Corners[1].position.x, Corners[2].position.x, Corners[3].position.x);
    //    maxs.y = Mathf.Max(Corners[0].position.y, Corners[1].position.y, Corners[2].position.y, Corners[3].position.y);

    //    Debug.Log(maxs);
    //    Debug.Log(mins);
    //    if (pos.x > mins.x && pos.x < maxs.x && pos.y < maxs.y && pos.y > mins.y)
    //        return true;
    //    else
    //        return false;
    //}
}
