﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PongScore : MonoBehaviour
{

    public static float LeftScore;
    public static float RightScore;

    int ColorChangerRate = 5;

    float prevLeftScore;
    float prevRightScore;

    public TextMeshProUGUI leftScore;
    public TextMeshProUGUI rightScore;

    public Material mat;
    public Material bkgr;

    public Ball ball;

    private void Start()
    {
        mat.color = Color.white;
    }

    void Update()
    {
        if (LeftScore != prevLeftScore)
        {
            //update left score text
            leftScore.text = LeftScore.ToString();
            //Debug.Log(LeftScore);

            if (LeftScore >= ColorChangerRate)
            {
                Color col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                mat.color = col;
                bkgr.color = new Color(1.0f - col.r, 1.0f - col.g, 1.0f - col.b);
            }
            if (LeftScore == 0)
            {
                mat.color = Color.white;
                bkgr.color = Color.black;
            }
            prevLeftScore = LeftScore;
        }
        if (RightScore != prevRightScore)
        {
            //update right score text
            rightScore.text = RightScore.ToString();
            //Debug.Log(RightScore);

            if (RightScore >= ColorChangerRate)
            {
                Color col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                mat.color = col;
                bkgr.color = new Color(1.0f - col.r, 1.0f - col.g, 1.0f - col.b);
            }
            if (RightScore == 0)
            {
                mat.color = Color.white;
                bkgr.color = Color.black;
            }
            prevRightScore = RightScore;
        }

        if (ball.isInsideBorder() == false)
        {
            Debug.Log("Ball out of bounds");
            ball.restartVelocity();
            restartScore();
        }
    }

    void restartScore()
    {
        LeftScore = 0;
        prevLeftScore = 0;

        RightScore = 0;
        prevRightScore = 0;

        leftScore.text = LeftScore.ToString();
        rightScore.text = RightScore.ToString();
    }
}