using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioPlayer : MonoBehaviour
{

    Rigidbody2D rb;
    Animator anim;

    public int Speed = 10;
    public int JumpSpeed = 300;

    bool canJump = true;

    float lastY;

    bool dead = false;

    public MarioTrigger feet;
    public MarioTrigger head;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            float x = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(x * Speed, rb.velocity.y);
            //rb.AddForce(x * Vector2.right * Spd);

            if (canJump && (Input.GetAxisRaw("Jump") > 0 || Input.GetAxisRaw("Vertical") > 0))
            {
                //rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                rb.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);

                canJump = false;
            }

            if (x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            anim.SetFloat("X", x);
            //Debug.Log("Actual Y: " + transform.position.y);
            //Debug.Log("Last Y: " + lastY);
            float dif = transform.position.y - lastY;
            anim.SetFloat("DifferenceY", dif);
            if (dif != 0)
            {
                anim.SetBool("Jump", true);
            }
            else
            {
                anim.SetBool("Jump", false);
            }
            //Debug.Log("Difference: " + dif);

            lastY = transform.position.y;
            //anim.SetBool("Jump", !canJump);
        }

        if (!canJump && (feet.TriggeredTag == "Mario/Ground" || feet.TriggeredTag == "Mario/Blocks/Brick1" || feet.TriggeredTag == "Mario/Blocks/Question1"))
        {
            canJump = true;
        }

        if (head.TriggeredTag == "Mario/Blocks/Brick1")
        {
            CreditsManager.ind++;

            Destroy(head.TriggeredGO);
        }

        if (head.TriggeredTag == "Mario/Blocks/Question1")
        {
            head.TriggeredGO.GetComponent<Animator>().SetTrigger("Hit");
        }

        if (feet.TriggeredTag == "Mario/Goomba")
        {
            CreditsManager.ind++;

            feet.TriggeredGO.GetComponent<MarioGoomba>().Die();
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Mario/Goomba"))
        {
            Die();
        }
        if (other.gameObject.CompareTag("Mario/PiranhaPlant"))
        {
            Die();
        }

        if (other.gameObject.CompareTag("Mario/Blocks/FinalFlag"))
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("Final");
            dead = true;
            rb.velocity = Vector2.right * Speed;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (other.gameObject.CompareTag("Mario/EnterCastle"))
        {
            StartCoroutine(FinishMario());
        }
    }

    IEnumerator FinishMario()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Main Menu");
    }

    public void Die()
    {
        rb.freezeRotation = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        anim.SetBool("Dead", true);
        float randX = Random.Range(-10f, 10f);
        float randY = Random.Range(3f, 5f);
        rb.AddForce(new Vector2(randX, randY), ForceMode2D.Impulse);
        dead = true;
        StartCoroutine(ReloadScene());
    }


    IEnumerator ReloadScene()
    {
        yield return new WaitForSecondsRealtime(3f);

        Debug.Log("ReloadScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
