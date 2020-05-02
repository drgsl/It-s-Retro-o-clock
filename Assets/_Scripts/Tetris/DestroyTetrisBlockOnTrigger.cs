using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTetrisBlockOnTrigger : MonoBehaviour
{
    //public Spawner spawner;

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        //Debug.Log(col.transform.parent.childCount);
        //if (col.transform.parent.childCount == 1)
        //{
        //    Destroy(col.gameObject);
        //    spawner.SpawnNext();
        //}
        //else
        //{
        //    Destroy(col.gameObject);
        //}

        //Transform temp = col.gameObject.transform.parent;

        //if (col.transform.parent.childCount > 1)
        //{
        //    Destroy(col.gameObject);
        //}
        if (col.transform.name == "block")
            if (col.transform.parent.GetComponent<Group>() && col.transform.parent.GetComponent<Group>().enabled == false)
            {
                Destroy(col.gameObject);
            }

        //Debug.Log(temp.childCount);

        //if (temp.childCount <= 0)
        //{
        //    spawner.SpawnNext();
        //}

    }
}
