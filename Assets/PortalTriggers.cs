using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTriggers : MonoBehaviour
{

    public Animator Doors1Anim;

    public Animator Elevator1Anim;

    public Transform AfterDoors;

    public AudioClip MarioMusic;
    public AudioClip MarioSFX;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Main/Player"))
        {
            if (gameObject.CompareTag("Portal/Triggers/Button1"))
            {
                if (Doors1Anim)
                {
                    Doors1Anim.SetTrigger("Open");
                }
            }

            if (gameObject.CompareTag("Portal/Triggers/Elevator1"))
            {
                if (Elevator1Anim)
                {
                    Elevator1Anim.SetTrigger("GoUp");
                }
            }

            if (gameObject.CompareTag("Portal/Triggers/Door1"))
            {
                if (AfterDoors)
                {
                    other.GetComponent<CharacterController>().enabled = false;
                    other.transform.position = AfterDoors.position;
                    other.GetComponent<CharacterController>().enabled = true;
                }
            }
            if (gameObject.CompareTag("Portal/Triggers/Finish"))
            {
                StartCoroutine(StartCredits());
            }
        }
    }

    IEnumerator StartCredits()
    {
        SoundManager.UpdateMusic(MarioMusic, 1f);
        SoundManager.UpdateSFX(MarioSFX, 0f);

        yield return new WaitForSecondsRealtime(3f);

        SceneManager.LoadScene(sceneName: "Mario Credits");

    }

}
