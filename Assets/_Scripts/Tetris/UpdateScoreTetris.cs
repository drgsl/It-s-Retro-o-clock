using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScoreTetris : MonoBehaviour
{

    public static TextMeshProUGUI scoretext;
    public static TextMeshProUGUI HighScoretext;

    public static int score;

    static GameObject DVDEnemy;
    static int ScoreThreshold = 100;

    static bool enemy1Spawned = false;
    static bool enemy2Spawned = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject.FindGameObjectWithTag("UI/GameOverTetris").transform.GetChild(0).gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        scoretext = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        HighScoretext = GameObject.FindGameObjectWithTag("HIGHSCORES/ingame/Tetris").GetComponent<TextMeshProUGUI>();
        //DVDEnemy = GameObject.FindGameObjectWithTag("DVDEnemy");
        DVDEnemy = (GameObject)Resources.Load("Tetris/DVD Enemy", typeof(GameObject));

        score = 0;

        Time.timeScale = 1f;

        if (GlobalScoreManager.Tetris < 0)
        {
            GlobalScoreManager.Tetris = 0;
        }

        updateScoreText();
    }

    public static void UpdateScore(int points)
    {
        score += points;
        if (score >= ScoreThreshold && !enemy1Spawned)
        {
            //DVDEnemy.SetActive(true);
            Instantiate(DVDEnemy);
            enemy1Spawned = true;
        }
        if (score >= 4 * ScoreThreshold && !enemy2Spawned)
        {
            Instantiate(DVDEnemy); 
            enemy2Spawned = true;
        }
        updateScoreText();
        Time.timeScale += 0.08f;
    }

    public static void updateScoreText()
    {
        scoretext.text = "Score: " + score;
        //Debug.Log(score);
        //Debug.Log("OldHighScore" + GlobalScoreManager.Tetris);
        if (score > GlobalScoreManager.Tetris)
        {
            GlobalScoreManager.Tetris = score;

            //Debug.Log("New Tetris HighScore!" + GlobalScoreManager.Tetris);
        }
        HighScoretext.text = "HIGHSCORE: " + GlobalScoreManager.Tetris;
    }

}
