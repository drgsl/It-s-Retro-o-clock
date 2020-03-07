using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTetrisBlockOnTrigger : MonoBehaviour
{

    bool canAdd = true;
    public GameObject block;
    public const float maximumTime = 2f;
    float timeLeft = maximumTime;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "block")
            if (col.transform.parent.GetComponent<Group>() && col.transform.parent.GetComponent<Group>().enabled == false)
            {
                canAdd = false;
            }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.name == "block")
            if (col.transform.parent.GetComponent<Group>() && col.transform.parent.GetComponent<Group>().enabled == false)
            {
                canAdd = true;
            }
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (canAdd && timeLeft <= 0)
        {
            GameObject go = Instantiate(block, transform.position, Quaternion.identity);
            //check if valid pos
            if (go.GetComponent<Group>().isValidGridPos())
            {
                //update block matrix
                go.GetComponent<Group>().updateGrid();
            }
            else
            {
                Destroy(go);
            }
            go.GetComponent<Group>().enabled = false; 
            timeLeft = maximumTime;
        }
    }
}
