using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public int health = 8;
    [SerializeField]
    private int attackSpeed = 2;
    [SerializeField]
    private int attackDamage = 2;

    [Header("AI")]
    public float wanderRadius;
    public float wanderTimer;
    [SerializeField]
    private float stoppingDistance = 0.8f;
    private float timer;
    private float attackTimer = 0f;

    private NavMeshAgent agent;   
    private EnemyFieldOfView fov;
    private Animator animator;

    //States
    private bool isWandering = true;
    private bool isAttacking = false;
    private bool isDead = false;

    [SerializeField]
    PlayerStatsScriptableObject playerData;

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
        SwingTimer();
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
        if (IsAtTarget() && isAttacking && attackTimer <= 0)
        {
            attackTimer = attackSpeed;
            playerData.health -= attackDamage;
            animator.SetTrigger("attack");
        }
    }

    private void SwingTimer()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
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


    private bool IsAtTarget()
    {
        if (fov.visibleTargets.Count > 0)
        {
            float dist = Vector3.Distance(fov.visibleTargets[0].transform.position, transform.position);
            if (dist <= 1.5f)
            {
                return true;
            }
        }
        return false;
    }
}
