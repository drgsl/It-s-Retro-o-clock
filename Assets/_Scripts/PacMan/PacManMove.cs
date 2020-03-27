using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PacManMove : MonoBehaviour
{

    Rigidbody rb;
    int speed = 10;

    Animator anim;

    float score = 0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighScoreText;

    GameObject WinningScreen;

    const int PacDotsCounter = 457;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        HighScoreText.text = "HIGHSCORE: " + GlobalScoreManager.PacMan;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InvokeRepeating("CheckDone", 60f, 1f);

        WinningScreen = GameObject.FindGameObjectWithTag("UI/WinningScreen");
        WinningScreen.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int horiz = (int)Input.GetAxisRaw("Horizontal");
        int vert = (int)Input.GetAxisRaw("Vertical");
        //if (horiz != vert && horiz != -1 * vert) // not moving diagonaly
        {
            anim.SetFloat("DirX", horiz);
            anim.SetFloat("DirY", vert);
            rb.velocity = new Vector3(horiz,0, vert).normalized * speed;
            //transform.position += new Vector3(horiz, vert).normalized * 0.8f;
        }
        if (horiz == vert && horiz == 0)
        {
            rb.velocity = new Vector3(horiz,0, vert).normalized * speed;
            //transform.position += new Vector3(horiz, vert).normalized * 0.8f;
        }
    }

    void CheckDone()
    {

        if (score >= PacDotsCounter)
        {
            WinningScreen.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pac-Man/PacDot")
        {
            Destroy(other.gameObject);
            //update score
            score += 1f;
            scoreText.text = "SCORE: " + score;

            if (score >= GlobalScoreManager.PacMan)
            {
                GlobalScoreManager.PacMan = score;
                HighScoreText.text = "HIGHSCORE: " + GlobalScoreManager.PacMan;
            }
        }

        if (other.tag == "Pac-Man/Ghost")
        {
            score = 0;
            GameObject.FindGameObjectWithTag("UI/Level Menu").GetComponent<LevelMenu>().RestartLevel();
        }
    }
}
