using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArkanoidScore : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    public static float newScore = 0f;
    float lastScore = 0f;
    public static float brickScore = 5f;
    public static float underLevelScore = 50f;

    void Update()
    {
        if (newScore != lastScore)
        {
            scoreText.text = newScore.ToString();
            lastScore = newScore;
        }
    }
}
