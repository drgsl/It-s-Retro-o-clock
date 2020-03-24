using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtleCameraMovement : MonoBehaviour
{

    Animator anim;

    float sens = 0.6f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        transform.localRotation = Quaternion.Euler(new Vector3(-1f * mouseY / (Screen.height * sens), mouseX / (Screen.width * sens), transform.rotation.z));
    }

    public void DisableAnimator()
    {
        anim.enabled = false;   
    }

}
