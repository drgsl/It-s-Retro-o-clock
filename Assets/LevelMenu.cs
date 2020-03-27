using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    GameObject lvlMenu;

    string lvlMenuTag = "UI/Level Menu";

    Scene CityScene;
    Scene MainMenuScene;

    GameObject gameOver;

    public TextMeshProUGUI loadingText;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(lvlMenuTag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        GameObject.FindGameObjectWithTag("UI/GameOverTetris").transform.GetChild(0).gameObject.SetActive(false);

        CityScene = SceneManager.GetSceneByName("City");
        MainMenuScene = SceneManager.GetSceneByName("Main Menu");

        gameOver = GameObject.FindGameObjectWithTag("UI/GameOverTetris").transform.GetChild(0).gameObject;
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
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("City"))
        {
            GameObject.FindGameObjectWithTag("UI/WinningScreen").SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))// || Input.GetKeyDown(KeyCode.T))
        {
            TogglePauseMenu();
        }

        Scene activeScene = SceneManager.GetActiveScene();

        if (Input.GetKeyDown(KeyCode.R) && activeScene != CityScene && activeScene != MainMenuScene)
        {
            RestartLevel();
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

        if (gameOver != null)
        {
            gameOver.SetActive(true);
        }
        if (SceneManager.GetActiveScene() == CityScene)
        {
            Cursor.visible = true;
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            Cursor.visible = false;
            //Debug.Log("Starting scene");
            SceneManager.LoadScene("City");//StartCoroutine(LoadAsynchronously("city"));
        }
        Destroy(this.gameObject);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //IEnumerator LoadAsynchronously(string sceneName)
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
    //    while (!operation.isDone)
    //    {
    //        float progress = Mathf.Clamp01 (operation.progress / .9f);
    //        loadingText.text = "" + progress;
    //        Debug.Log(progress);
    //        yield return null;
    //    }
    //}
}
