using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    GameObject lvlMenu;

    string lvlMenuTag = "UI/Level Menu";

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(lvlMenuTag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        lvlMenu = GameObject.FindGameObjectWithTag(lvlMenuTag).transform.GetChild(0).gameObject;
        lvlMenu.SetActive(false);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.T))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        lvlMenu.SetActive(!lvlMenu.activeSelf);
        Time.timeScale = System.Convert.ToSingle(!lvlMenu.activeSelf);
        Cursor.lockState = 1 - Cursor.lockState; //Debug.Log(Cursor.lockState);
        Cursor.visible = !Cursor.visible;
    }

    public void StopButton()
    {
        Time.timeScale = 1f;

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("City"))
        {
            Cursor.visible = true;
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            Cursor.visible = false;
            SceneManager.LoadScene("City");
        }
        Destroy(this.gameObject);
    }
}
