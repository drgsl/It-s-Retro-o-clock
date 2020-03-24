using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTriggers : MonoBehaviour
{


    TMP_Text InteractableText;
    public static KeyCode loadLvlButton = KeyCode.E;
    public static KeyCode InteractButton = KeyCode.F;
    string gameName = "";

    Animator targetAnim;

    bool loadNewScene = false;

    private void Start()
    {
        InteractableText = GameObject.FindGameObjectWithTag("Trigger/City/Text").GetComponent<TextMeshProUGUI>();
        InteractableText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (targetAnim != null)
        {
            if (Input.GetKeyDown(InteractButton))
            {
                targetAnim.SetBool("OpenDoor", !targetAnim.GetBool("OpenDoor"));
                InteractableText.text = "";
                targetAnim = null;
            }
        }

        if (loadNewScene)
        {
            if (Input.GetKeyDown(loadLvlButton) && gameName != "")
            {
                SceneManager.LoadScene(gameName);

                InteractableText.text = "";
                loadNewScene = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        int prefixInd = 13;

        if (other.tag.StartsWith("Trigger/City/"))
        {
            gameName = other.tag.Substring(prefixInd, other.tag.Length - prefixInd);
            InteractableText.text = "Press " + char.ToUpper((char)loadLvlButton) + " to visit " + gameName;
            loadNewScene = true;
        }

        if (other.tag.Equals("Trigger/SnakeDoor"))
        {
            targetAnim = other.GetComponent<Animator>();

            if (targetAnim.GetBool("OpenDoor") == true)
            {
                InteractableText.text = "Press " + char.ToUpper((char)InteractButton) + " to close the prison door";
            }
            else
            {
                InteractableText.text = "Press " + char.ToUpper((char)InteractButton) + " to open the prison door";
            }
        }

        if (other.tag.Equals("Trigger/Laptop"))
        {
            targetAnim = other.GetComponent<Animator>();
            targetAnim.SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Trigger/Laptop"))
        {
            targetAnim = other.GetComponent<Animator>();
            targetAnim.SetBool("Open", false);
        }
        targetAnim = null;

        if (other.tag.StartsWith("Trigger/City/"))
        {
            InteractableText.text = "";
            gameName = "";
            loadNewScene = false;
        }
    }

}
