using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagoneAudience : MonoBehaviour
{
    Animator[] anims;

    // Start is called before the first frame update
    void Start()
    {
        anims = GetComponentsInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int index = Random.Range(0, anims.Length);
        anims[index].SetTrigger("Cheer");
    }
}
