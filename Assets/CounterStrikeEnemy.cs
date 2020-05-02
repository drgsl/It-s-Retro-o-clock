using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CounterStrikeEnemy : MonoBehaviour
{
    Transform PlayerTransform;
    NavMeshAgent nav;

    Animator anim;

    public int MaxHealth = 100;

    public int Health;
    bool isDead = false;
    bool didShoot = false;

    public int Damage = 5;

    public Animator gunAnim;
    float DespawnRate = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        PlayerTransform = GameObject.FindGameObjectWithTag("Main/Player").GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        gunAnim = transform.GetChild(2).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0 && !isDead)
        {
            Die();
        }
        else if(!isDead)
        {
            Vector3 startPos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            //Vector3 lookPos = new Vector3(3f, PlayerTransform.position.y, PlayerTransform.position.z);

            RaycastHit[] hits = Physics.RaycastAll(startPos, transform.TransformDirection(Vector3.forward), 50f -1);
            for (int i = 0; i < hits.Length; i++)
            {
                Debug.Log(hits[i].transform.tag);
                Debug.DrawRay(startPos, transform.TransformDirection(Vector3.forward) * hits[i].distance, Color.yellow);
                if (hits[i].transform.CompareTag("Main/Player") && !didShoot)
                {
                    StartCoroutine(Shoot());
                    //Debug.Log("Shot " + hit.transform.gameObject);
                }
            }

            nav.destination = PlayerTransform.position;
        }
    }

    IEnumerator Shoot()
    {
        PlayerHealth.Health -= Damage;
        PlayerHealth.updateHealth();
        Debug.Log("Shot ");
        gunAnim.SetTrigger("Shoot");
        playSound();
        didShoot = true;

        yield return new WaitForSeconds(1f);
        didShoot = false;
    }

    void Die()
    {
        if (anim.GetBool("Dead") == false)
            anim.SetBool("Dead", true);
        nav.enabled = false;

        transform.GetChild(0).GetComponent<Collider>().enabled = false;
        transform.GetChild(1).GetComponent<Collider>().enabled = false;
        transform.GetChild(2).gameObject.SetActive(false);

        isDead = true;

        StartCoroutine(Despawn());
    }

    public void playSound()
    {
        gunAnim.gameObject.GetComponent<AudioSource>().Play();
    }


    IEnumerator Despawn()
    {
        yield return new WaitForSecondsRealtime(DespawnRate);
        Destroy(gameObject);
    }
}
