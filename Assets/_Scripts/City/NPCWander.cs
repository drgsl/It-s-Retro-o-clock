using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWander : MonoBehaviour
{
    const float MaxTime = 60f;
    float timer = MaxTime;


    public float wanderRadius = 500f;

    float distThreshold = 20f;

    public bool isCar = false;

    private NavMeshAgent agent;
    Animator anim;

    Vector3 oldPos;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        //InvokeRepeating("Wander", 0f, wanderTimer);
        anim = transform.GetComponent<Animator>();
        if (transform.tag == "NPC/Car")
            isCar = true;
        else
            isCar = false;

        Wander();

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //float dist = Vector3.Distance(transform.position, oldPos); 

        // avoiding the sqrt from the distance formula
        float dist = (transform.position.x - oldPos.x) * (transform.position.x - oldPos.x);
        dist += (transform.position.y - oldPos.y) * (transform.position.y - oldPos.y);
        dist += (transform.position.z - oldPos.z) * (transform.position.z - oldPos.z);
        dist = Mathf.Abs(dist);

        if (timer <= 0 || dist < distThreshold * distThreshold)
        {
            Wander();
            timer = MaxTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }

    void Wander()
    {
        int AreaMask;
        if (isCar)
            AreaMask = NavMesh.GetAreaFromName("Road");
        else 
            AreaMask = NavMesh.GetAreaFromName("Sidewalk");
        oldPos = RandomNavSphere(transform.position, wanderRadius, 1 << AreaMask);
        agent.SetDestination(oldPos);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
