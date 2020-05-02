using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioTrigger : MonoBehaviour
{
    public string TriggeredTag;

    public GameObject TriggeredGO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggeredTag = collision.gameObject.tag;
        TriggeredGO = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            TriggeredTag = "";
            TriggeredGO = null;
    }

}
