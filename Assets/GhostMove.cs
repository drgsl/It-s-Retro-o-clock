using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GhostMove : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;

    private NavMeshAgent agent;
    Animator anim;
    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("Wander", 0f, wanderTimer);
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Wander()
    {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
        anim.SetFloat("DirX", Random.Range(-1f,1f));
        anim.SetFloat("DirY", Random.Range(-1f,1f));
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
