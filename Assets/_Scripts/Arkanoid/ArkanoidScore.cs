using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArkanoidScore : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public static float newScore = 0f;
    float lastScore = 0f;
    public static float brickScore = 1f;
    public static float underLevelScore = 50f;

    private void Start()
    {
        newScore = 0f;
        scoreText.text = "";
        if (GlobalScoreManager.BrickBreaker < 0)
        {
            GlobalScoreManager.BrickBreaker = 0f;
        }
        highScoreText.text = "HIGHSCORE: " + GlobalScoreManager.BrickBreaker;
    }

    void Update()
    {
        if (newScore != lastScore)
        {
            if (newScore > GlobalScoreManager.BrickBreaker)
            {
                GlobalScoreManager.BrickBreaker = newScore;
                highScoreText.text = "HIGHSCORE: " + GlobalScoreManager.BrickBreaker;
            }
            scoreText.text =  "Score : " + newScore;
            lastScore = newScore;
        }
    }
}
