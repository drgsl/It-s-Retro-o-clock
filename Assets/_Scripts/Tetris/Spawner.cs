using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] usableGroup;
    public GameObject[] nextGroup;

    public int nextInd = -1;

    public int currentInd = -1;

    public GameObject frame;
    GameObject go;

    public void SpawnNext()
    {
        
        if(go)Destroy(go);

        Instantiate(usableGroup[currentInd], transform.position, Quaternion.identity);

        //show next group

        go = Instantiate(nextGroup[nextInd], frame.transform.position, Quaternion.identity);
        
        //Debug.Log(groups[nextInd].transform.name);

        //update currentInd & nextInd
        currentInd = nextInd;
        nextInd = Random.Range(0, usableGroup.Length);
    }

    void Start()
    {
        currentInd = Random.Range(0, usableGroup.Length);

        //usableGroup = new GameObject[Resources.LoadAll<GameObject>("NextGroups").Length];
        //for (int i = 0; i < usableGroup.Length; i++)
        //{
        //    Debug.Log(usableGroup[i]);
        //}

        //spawn initial group
        SpawnNext();


    }

}
