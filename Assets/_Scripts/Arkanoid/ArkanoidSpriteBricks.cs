using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkanoidSpriteBricks : MonoBehaviour
{

    public GameObject[] sprites;

    public int BrickWidth;
    public int BrickHeight;

    const float Scale_Factor = 70f;
    Vector3 scale = new Vector3(Scale_Factor, Scale_Factor, 0f);

    Vector3 startPos;

    const float Max_Time = 5;
    //const float Half_Time = Max_Time / 2;
    //float timeLeft = Max_Time;

    string LeftUp = "LeftUp";
    string RightUp = "RightUp";
    string LeftDown = "LeftDown";
    string RightDown = "RightDown";
    string Center = "Center";

    float minX = -190;
    float maxX = 190;

    int stage = 0;

    GameObject winningScreen;
    void Start()
    {
        InvokeRepeating("StartPhase", 2f, .01f); // starting after 2 seconds, repeating every .3 seconds
        
        InvokeRepeating("CheckDone", 60f, 1f);

        winningScreen = GameObject.FindGameObjectWithTag("UI/WinningScreen");
        winningScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        startPos = transform.GetChild(0).transform.position;
        int ind = 1;
        for (int j = 0; j >= -BrickWidth - 1; j--) //-Mathf.Sqrt(transform.childCount - 1)
        {
            for (int i = 0; i < BrickHeight; i++) //Mathf.Sqrt(transform.childCount - 1)
            {
                if (ind == transform.childCount)
                    break;
                Transform child = transform.GetChild(ind);
                ind++;
                child.transform.localScale = scale;
                //child.transform.name = "i = " + i.ToString() + " , j = " +  j.ToString();

                if (i <= 7 && j > -14)
                    child.transform.name = LeftUp;
                else if (i > 7 && j > -14)
                    child.transform.name = RightUp;
                else if (i <= 7 && j <= -14)
                    child.transform.name = LeftDown;
                else if (i > 7 && j <= -14)
                    child.transform.name = RightDown;
                else
                    child.transform.name = Center;
                child.gameObject.AddComponent<BoxCollider2D>();
                BoxCollider2D boxCollider = child.gameObject.GetComponent<BoxCollider2D>();
                Vector2 spriteSizes = new Vector2(boxCollider.size.x, boxCollider.size.y) * Scale_Factor; // needs improvement
                child.transform.position = new Vector3(startPos.x + i * spriteSizes.x, startPos.y + j * spriteSizes.y, 0);
                BrickArkanoid.RandomizeColors = false;
                child.gameObject.AddComponent<BrickArkanoid>();

                child.GetComponent<BrickArkanoid>().startPos = child.transform.position;
            }
        }
    }

    void CheckDone()
    {
        if (transform.childCount == 1)
        {
            winningScreen.SetActive(true);
        }
    }

    //private void Update()
    //{
    //    if (timeLeft <= 0)
    //    {
    //        //StartPhase(0);
    //        timeLeft = Max_Time;
    //    }
    //    if (timeLeft <= Half_Time)
    //    {
    //        //StartPhase(1);
    //    }
    //    timeLeft -= Time.deltaTime;
    //}

    void StartPhase()
    {
        //Debug.Log(stage);
        switch (stage)
        {
            case 0:
                Phase0();
                break;
            case 1:
                Phase1();
                break;

            default:
                break;
        }
    }

    void Phase0()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = transform.GetChild(i).GetComponent<BrickArkanoid>().startPos;
        }
        stage = 1;
    }

    void Phase1()
    {
        bool canMove;
        float animSpeed = .5f;
        for (int i = 1; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i).transform;
            canMove = true;
            if (child.name == RightUp && canMove)
            {
                child.position += new Vector3(animSpeed, 0, 0);
                if (child.position.x > maxX)
                {
                    child.name = LeftUp;
                    canMove = false;
                }
            }
            if (child.name == LeftUp && canMove)
            {
                child.position += new Vector3(-animSpeed, 0, 0);
                if (child.position.x < minX)
                {
                    child.name = RightUp;
                    canMove = false;
                }
            }
            if (child.name == LeftDown && canMove)
            {
                child.position += new Vector3(-animSpeed / 1.5f, 0, 0);
                if (child.position.x < minX)
                {
                    child.name = RightDown;
                    canMove = false;
                }
            }
            if (child.name == RightDown && canMove)
            {
                child.position += new Vector3(animSpeed / 1.5f, 0, 0);
                if (child.position.x > maxX)
                {
                    child.name = LeftDown;
                    canMove = false;
                }
            }
        }
    }
}
