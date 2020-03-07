using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScoreTetris : MonoBehaviour
{

    public static TextMeshProUGUI scoretext;
    public static int score;

    static GameObject DVDEnemy;
    static int ScoreThreshold = 40;

    static bool enemy1Spawned = false;
    static bool enemy2Spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        scoretext = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        //DVDEnemy = GameObject.FindGameObjectWithTag("DVDEnemy");
        DVDEnemy = (GameObject)Resources.Load("Tetris/DVD Enemy", typeof(GameObject));

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
        if (score >= 2 * ScoreThreshold && !enemy2Spawned)
        {
            Instantiate(DVDEnemy); 
            enemy2Spawned = true;
        }
        updateScoreText();
        Time.timeScale += 0.05f;
    }

    public static void updateScoreText()
    {
        scoretext.text = "Score: " + score;
    }

}
