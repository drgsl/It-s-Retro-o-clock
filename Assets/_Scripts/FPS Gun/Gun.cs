using UnityEngine;

public class Gun : MonoBehaviour
{

    public float damage = 25f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    public Camera fpsCam;

    public float nextTimeToFire = 0f;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            if (anim)
            {
                anim.SetTrigger("Shooting");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (anim)
            {
                anim.speed = 0.1f;
                anim.SetTrigger("Reloading");
            }
        }
    }

    public void Shoot()
    {
        //playSound();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            //CounterStrikeEnemy enemy = hit.transform.GetComponent<CounterStrikeEnemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.transform.CompareTag("Enemy/Body Collider"))
            {
                CounterStrikeEnemy enemy = hit.transform.parent.GetComponent<CounterStrikeEnemy>();
                enemy.Health -= (int)damage;
            }

            if (hit.transform.CompareTag("Enemy/Head Collider"))
            {
                //Headshot
                CounterStrikeEnemy enemy = hit.transform.parent.GetComponent<CounterStrikeEnemy>();
                enemy.Health -= enemy.MaxHealth;
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }

    public void playSound()
    {
       gameObject.GetComponent<AudioSource>().Play();
    }
}
