using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator animator;
    public int Health = 5;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool dead;
    public float speed = 2.0f;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check if player is within sight range or attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && !dead) Patroling();
        if (playerInSightRange && !playerInAttackRange && !dead) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && !dead) AttackPlayer();

        if(Health < 1){
            animator.SetBool("dead", dead=true);
            agent.SetDestination(transform.position);
            StartCoroutine(Dead(4.0f));
        }
    }

    private void Patroling()
    {
        // Optional: Add patrol logic here
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetFloat("Speed", speed);
        
    }

    private void AttackPlayer()
    {
        // Stop AI from moving
        speed = 0.0f;
        agent.SetDestination(transform.position);
        animator.SetTrigger("attack");
        StartCoroutine(Stop(0.1f));        // Optional: Add attack logic here (e.g., play an attack animation)

        // Ensure the AI is facing the player
        transform.LookAt(player);
    }

    IEnumerator Dead(float delay){
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    IEnumerator Stop(float delay){
        yield return new WaitForSeconds(delay);
        animator.ResetTrigger("attack");
        speed = 2.0f;
    }
}
