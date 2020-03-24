using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDotsSpawner : MonoBehaviour
{
    Vector3 start = new Vector3(-26, 0, 27);
    Vector3 stop = new Vector3(22, 0,  -26);

    Vector3 space = Vector3.one * 2f;
    public GameObject pacDot;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = (int)start.x; x <= stop.x; x+=(int)space.x)
        {
            for (int z = (int)start.z; z >= stop.z; z-=(int)space.z)
            {
             Instantiate(pacDot, new Vector3(x, 0, z), Quaternion.identity);
            }
        }    
    }
}
