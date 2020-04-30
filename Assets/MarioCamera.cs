using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioCamera : MonoBehaviour
{

    Transform player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Mario/Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }
}
