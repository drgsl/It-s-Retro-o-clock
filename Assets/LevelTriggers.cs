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
    public static KeyCode PoolButton = KeyCode.Z;
    string gameName = "";

    Animator targetAnim;

    bool loadNewScene = false;

    char[] randomChars = new char[] { '¶', '♪', '☺', '☻', '☻', '♣', '♥', '♦', '•', '◘', '◙', '♂', '♀', '♫', '☼', '►', '▲', '◄', '▼', '↕', '‼', '§', '↨', '▬'};

    bool atPool = false;

    //public GameObject hands;
    //public GameObject AK47;

    //public GameObject FirstLevel;
    //public GameObject SecondLevel;

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

            if (atPool)
            {
                if (Input.GetKeyDown(PoolButton))
                {
                    targetAnim.SetTrigger("Activate");
                    InteractableText.text = "";
                    atPool = false;
                    targetAnim = null;
                }
                //StartCoroutine(SecretTextAnim());
            }

        }

        if (loadNewScene)
        {
            if (Input.GetKeyDown(loadLvlButton) && gameName != "")
            {
                LevelLoader.LoadScene(gameName);
                //SceneManager.LoadScene(gameName);

                InteractableText.text = "";
                loadNewScene = false;
            }
        }


    }

    IEnumerator SecretTextAnim(int cycle, bool ascending)
    {
        InteractableText.text = "";
        for (int i = 0; i <= cycle; i++)
        {
            InteractableText.text += randomChars[Random.Range(0, randomChars.Length)];
        }

        InteractableText.text += "  " + char.ToUpper((char)PoolButton) + "  ";

        for (int i = 0; i <= cycle; i++)
        {
            InteractableText.text += randomChars[Random.Range(0, randomChars.Length)];
        }

        yield return new WaitForSecondsRealtime(0.3f);

        if (ascending)
        {
            cycle++;
            if (cycle >= 6)
            {
                ascending = false;
            }
        }
        else
        {
            cycle--;
            if (cycle <=0)
            {
                ascending = true;
            }
        }

        
        if (atPool)
        {
            StartCoroutine(SecretTextAnim(cycle, ascending));
        }
        else
        {
            InteractableText.text = "";
            StopCoroutine(SecretTextAnim(cycle, ascending));
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Portal/PortalGun"))
        {
            ElevatorPoints.canStartPortal = true;
            InteractableText.text = "Press " + "E" + " to visit Portal";
        }

        if (other.tag.StartsWith("Trigger/City/"))
        {
            int prefixInd = 13;

            gameName = other.tag.Substring(prefixInd, other.tag.Length - prefixInd);
            InteractableText.text = "Press " + char.ToUpper((char)loadLvlButton) + " to visit " + gameName;
            loadNewScene = true;
        }

        if (other.CompareTag("Trigger/SnakeDoor"))
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

        if (other.CompareTag("Trigger/Laptop"))
        {
            targetAnim = other.GetComponent<Animator>();
            targetAnim.SetBool("Open", true);
        }

        if (other.CompareTag("Trigger/Pool") && GlobalScoreManager.FloorCompleted())
        {
            targetAnim = other.GetComponent<Animator>();
            atPool = true;
            StartCoroutine(SecretTextAnim(1, true));
        }

        if (other.CompareTag("Trigger/Tips/MarioStatue"))
        {
            InteractableText.text = "The fountain is a lie." +'\n' + " Come back stronger";
        }

        //if (other.tag.Equals("Trigger/CS/GiveWeapon"))
        //{
        //    AK47.SetActive(true);
        //    hands.SetActive(false);

        //    //hide City

        //    FirstLevel.SetActive(false);
        //}

        //if (other.tag.Equals("Trigger/CS/TakeWeapon"))
        //{
        //    AK47.SetActive(false);
        //    hands.SetActive(true);

        //    // hide cs

        //    FirstLevel.SetActive(true);
        //}
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Portal/PortalGun"))
        {
            ElevatorPoints.canStartPortal = false;
        }

        if (other.tag.Equals("Trigger/Laptop"))
        {
            targetAnim = other.GetComponent<Animator>();
            targetAnim.SetBool("Open", false);
        }


        if (other.tag.StartsWith("Trigger/City/"))
        {
            gameName = "";
            loadNewScene = false;
        }

        //if (other.tag.Equals("Trigger/SnakeDoor"))
        //{
        //    targetAnim = null;

        //    InteractableText.text = "";
        //}

        if (other.tag.Equals("Trigger/Pool"))
        {
            //targetAnim = null;
            atPool = false;
        }

        InteractableText.text = "";
        targetAnim = null;
    }

}
