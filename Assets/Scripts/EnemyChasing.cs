using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasing : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target; // The player's transform

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!PlayerManger.gameStart)
            return;
        // Set the destination of the NavMeshAgent to the player's position
        animator.SetBool("run", true);
        agent.SetDestination(target.position);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "fire")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
