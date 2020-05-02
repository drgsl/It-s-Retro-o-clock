using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioGround : MonoBehaviour
{
    public GameObject actualGround;

    public Transform player;

    public GameObject Before;
    public GameObject After;

    // Update is called once per frame
    void Update()
    {
        actualGround.transform.position = new Vector2(player.transform.position.x, actualGround.transform.position.y);


        if (player.transform.position.x >= After.transform.position.x)
        {
            //Debug.Log("Bigger than After");
            Before.transform.position = new Vector2(After.transform.position.x + 21 / 2, Before.transform.position.y);
        }

        if (player.transform.position.x >= Before.transform.position.x)
        {
            //Debug.Log("Bigger than Before");
            After.transform.position = new Vector2(Before.transform.position.x + 21 / 2, After.transform.position.y);
        }

        if (player.transform.position.x <= After.transform.position.x)
        {
            //Debug.Log("Smaller than After");
            Before.transform.position = new Vector2(After.transform.position.x - 21 / 2, Before.transform.position.y);
        }

        if (player.transform.position.x <= Before.transform.position.x)
        {
            //Debug.Log("Smaller than Before");
            After.transform.position = new Vector2(Before.transform.position.x - 21 / 2, After.transform.position.y);
        }
    }
}
