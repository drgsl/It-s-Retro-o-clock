using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScoreTetris : MonoBehaviour
{

    public static TextMeshProUGUI scoretext;
    public static int score;


    // Start is called before the first frame update
    void Start()
    {
        scoretext = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        updateScoreText();
    }

    public static void UpdateScore(int points)
    {
        score += points;
        updateScoreText();
    }

    public static void updateScoreText()
    {
        scoretext.text = "Score: " + score;
    }

}
