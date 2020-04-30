using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq; // because of the list

using TMPro;

public class Snake : MonoBehaviour
{

    Vector2 dir = Vector2.right;

    List<Transform> tail = new List<Transform>();

    bool ate = false;

    public GameObject tailPrefab;

    float speed = 1f;

    public SnakeSpawnFood spawner;

    //GameObject WinningScreen;

    int score = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.07f, 0.07f);
        highScoreText.text = "";
        scoreText.text = "";
        if (GlobalScoreManager.Snake < 0)
        {
            GlobalScoreManager.Snake = 0f;
        }
        highScoreText.text = "HIGHSCORE: " + GlobalScoreManager.Snake;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        //WinningScreen = GameObject.FindGameObjectWithTag("UI/WinningScreen");
        //WinningScreen.SetActive(false);
        LevelMenu.DeactivateWinning();
    }

    private void Update()
    {

        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        if (horiz != 0 || vert !=0)
        {
            dir = new Vector2(horiz, vert);
        }
    }
    void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir * speed);

        // Ate something? Then insert new Element into gap
        if (ate)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            if (Time.timeScale <= 5)
                Time.timeScale += 0.05f;

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.name.StartsWith("food"))
        {
            // Get longer in next Move call
            ate = true;
            spawner.SpawnFood();
            // Remove the Food
            Destroy(coll.gameObject);

            score += 1;
            scoreText.text = "SCORE: " + score;
            if (score > GlobalScoreManager.Snake)
            {
                GlobalScoreManager.Snake = score;
                highScoreText.text = "HIGHSCORE: " + GlobalScoreManager.Snake;
            }
        }
        // Collided with Tail or Border
        else
        {
            //Debug.Break();
            Time.timeScale = 1f;
            GameObject.FindGameObjectWithTag("UI/GameOverTetris").transform.GetChild(0).gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
