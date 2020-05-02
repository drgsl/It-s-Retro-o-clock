using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TronScoreManager : MonoBehaviour
{

    public static float score = 0f;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;

    private void Start()
    {
        score = 0;
        HighScoreText.text = "";

        GameObject.FindGameObjectWithTag("UI/GameOverTetris").transform.GetChild(0).gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }



    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
        if (GlobalScoreManager.Tron < score)
        {
            GlobalScoreManager.Tron = (int)score;
            HighScoreText.text = "NEW HIGHSCORE!";
        }
        ScoreText.text = "SCORE : " + Mathf.Floor(score);
    }
}
