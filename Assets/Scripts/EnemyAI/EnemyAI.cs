using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    [SerializeField]
    public int maxHealth = 8;   
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
    public EnemyHealthBar healthBar;
    private Loot loot;

    private NavMeshAgent agent;   
    private EnemyFieldOfView fov;
    private Animator animator;

    //States
    private bool isWandering = true;
    private bool isAttacking = false;
    public bool isDead = false;

    [SerializeField]
    Player player;

    private void Awake()
    {
        healthBar.SetMaxHealth(maxHealth);
        health = maxHealth;
    }

    // Use this for initialization
    void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<EnemyFieldOfView>();
        loot = GetComponent<Loot>();
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
            player.TakeDamage(attackDamage);
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
        SetHealthBar();
        if (health <= 0)
        {
            animator.SetTrigger("dead");
            isWandering = false;
            isAttacking = false;
            isDead = true;           
            loot.DropLoot();
            loot.DropGold();
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void SetHealthBar() 
    {       
        healthBar.SetHealth(health);
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
            if (dist <= stoppingDistance)
            {
                return true;
            }
        }
        return false;
    }
}
