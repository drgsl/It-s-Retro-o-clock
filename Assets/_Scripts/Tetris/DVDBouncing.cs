using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVDBouncing : MonoBehaviour
{

    public float width = 1.25f;  // dvd sprite width
    public float height = 0.75f; // dvd sprite height

    public float xSpeed;
    public float ySpeed;

    public float xPos;
    public float yPos;

    void Start()
    {
        xPos = Random.Range(0f, Playfield.w);
        yPos = Random.Range(2f, Playfield.h);
        xSpeed = 0.001f; 
        ySpeed = 0.001f;
        changeColor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            transform.position = new Vector3(xPos, yPos);

            xPos += xSpeed;
            yPos += ySpeed;

            checkX();
            checkY();
    }

    public void checkX()
    {
        if (xPos >= 8.25f)
        {
            xSpeed = -xSpeed;
            xPos = 8.25f;
            changeColor();
        }
        else if (xPos <= -0.5f)
        {
            xSpeed = -xSpeed;
            xPos = -0.5f;
            changeColor();
        }
    }

    public void checkY()
    {
        if (yPos >= 12.75f)
        {
            ySpeed = -ySpeed;
            yPos = 12.75f;
            changeColor();
        }
        else if (yPos <= 1.75f)
        {
            ySpeed = -ySpeed;
            yPos = 1.75f;
            changeColor();
        }
    }

    void changeColor()
    {
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
