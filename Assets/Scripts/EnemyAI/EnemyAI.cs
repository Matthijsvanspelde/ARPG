using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int health = 8;
    public float wanderRadius;
    public float wanderTimer;
    private NavMeshAgent agent;
    private float timer;
    private EnemyFieldOfView fov;
    private Animator animator;

    [SerializeField]
    private float stoppingDistance = 0.8f;

    private bool isWandering = true;
    private bool isAttacking = false;
    private bool isDead = false;
    // Use this for initialization
    void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<EnemyFieldOfView>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimationState();
        SetMoveDirection();
        if (isWandering)
        {
            Wander();
        }
        else if (isAttacking)
        {
            Attack();
        }
        
    }

    private void Wander()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            agent.stoppingDistance = 0;
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
        if (fov.FoundPlayer())
        {
            isWandering = false;
            isAttacking = true;
        }
    }

    private void Attack()
    {
        agent.stoppingDistance = stoppingDistance;
        if (fov.visibleTargets.Count > 0)
        {
            agent.destination = fov.visibleTargets[0].transform.position;
        }
        else
        {
            isWandering = true;
            isAttacking = false;
        }         
    }

    private void SetAnimationState() 
    {
        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else if (agent.velocity.magnitude == 0)
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            isWandering = false;
            isAttacking = false;
            isDead = true;
            animator.SetTrigger("dead");
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 RandomDirection = Random.insideUnitSphere * dist;
        RandomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(RandomDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    private void SetMoveDirection()
    {
        float velocity = agent.destination.x - transform.position.x;
        if (velocity > 0f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (velocity < 0f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }

}
