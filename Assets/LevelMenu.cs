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

    public GameObject PauseButtons;
    public GameObject HighScoreBackground;
    public TextMeshProUGUI HighScoreText;

    //public TextMeshProUGUI loadingText;

    public GameObject WinningScreen;

    public static LevelMenu instance;

    private void Awake()
    {
        instance = this;
        GameObject.FindGameObjectWithTag("UI/GameOverTetris").transform.GetChild(0).gameObject.SetActive(false);

        CityScene = SceneManager.GetSceneByName("City");
        MainMenuScene = SceneManager.GetSceneByName("Main Menu");

        gameOver = GameObject.FindGameObjectWithTag("UI/GameOverTetris").transform.GetChild(0).gameObject;

        CheckWinningScreen();
    }

    public void CheckWinningScreen()
    {
        if (WinningScreen == null)
        {
            WinningScreen = GameObject.FindGameObjectWithTag("UI/WinningScreen");
        }
        //if (WinningScreen == null)
        //{
        //    WinningScreen = transform.GetChild(3).gameObject;
        //}
        Debug.Log(WinningScreen);
    }

    public static void ActivateWinning()
    {
        instance.CheckWinningScreen();
        instance.WinningScreen.SetActive(true);
    }

    public static void DeactivateWinning()
    {
        instance.CheckWinningScreen();
        instance.WinningScreen.SetActive(false);
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
            //GameObject.FindGameObjectWithTag("UI/WinningScreen").SetActive(false);
            DeactivateWinning();
        }

        HideStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePauseMenu();
        }

        Scene activeScene = SceneManager.GetActiveScene();

        if (Input.GetKeyDown(KeyCode.R) && activeScene != CityScene && activeScene != MainMenuScene)
        {
            RestartLevel();
        }
    }

    public void ShowStats()
    {
        PauseButtons.SetActive(false);
        //HighScoreBackground.SetActive(true);
        HighScoreText.gameObject.SetActive(true);
        //HighScoreText.text = true;



        HighScoreText.text = "HIGHSCORES: " + '\n';

        CheckScore(GlobalScoreManager.Tetris, "Tetris: ");
        CheckScore(GlobalScoreManager.BrickBreaker, "Brick Breaker: ");
        CheckScore(GlobalScoreManager.Pong, "Pong: ");
        CheckScore(GlobalScoreManager.PacMan, "Pac-Man: ");
        CheckScore(GlobalScoreManager.Tron, "Tron: ");
        CheckScore(GlobalScoreManager.Snake, "Snake: ");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CheckScore(float GameScoreVar,string GameName)
    {
        string HiddenGame = "";
        string HiddenScore = "Ø";

        string GameNameStr;

        if (GameScoreVar >= 0)
        {
            GameNameStr = GameName + GameScoreVar + '\n';
        }
        else
        {
            for (int i = 0; i < GameName.Length - 2; i++)
            {
                HiddenGame += '█';
            }
            HiddenGame += ": ";
            GameNameStr = HiddenGame + HiddenScore + '\n';
        }

        HighScoreText.text += '\n' + GameNameStr;

    }

    public void HideStats()
    {
        if (PauseButtons) PauseButtons.SetActive(true);
        //HighScoreBackground.SetActive(false);
        if (HighScoreText)
        {
            HighScoreText.text = "";
            HighScoreText.gameObject.SetActive(false);
        }
    }

    public void TogglePauseMenu()
    {
        lvlMenu.SetActive(!lvlMenu.activeSelf);
        //Time.timeScale = System.Convert.ToSingle(!lvlMenu.activeSelf);
        //Cursor.lockState = 1 - Cursor.lockState; //Debug.Log(Cursor.lockState);
        //Cursor.visible = !Cursor.visible;

        if (lvlMenu.activeSelf == true)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            DeactivateWinning();
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        HideStats();
    }

    public void StopButton()
    {
        Time.timeScale = 1f;

        //if (gameOver != null)
        //{
        //    gameOver.SetActive(true);
        //}
        //Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "City" || SceneManager.GetActiveScene().name == "Main Menu")
        {
            //LevelLoader.LoadScene("Main Menu");
            TogglePauseMenu();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Debug.Log("going to main menu");
            LevelLoader.LoadScene("Main Menu");
            //SceneManager.LoadScene("Main Menu");
        }
        else
        {
            Cursor.visible = false;
            //Debug.Log("Starting scene");
            LevelLoader.LoadScene("City");
            TogglePauseMenu();
            //SceneManager.LoadScene("City");//StartCoroutine(LoadAsynchronously("city"));
        }
        //Destroy(this.gameObject);
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
