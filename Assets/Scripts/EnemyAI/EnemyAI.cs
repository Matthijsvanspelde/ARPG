using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public float health;
    [SerializeField]
    public float maxHealth = 8;   
    public float attackSpeed = 2;
    public float attackDamage = 2;
    [SerializeField]
    private int experienceReward = 5;

    [Header("AI")]
    public float wanderRadius;
    public float wanderTimer;
    public float attackRange = 0.8f;
    private float timer;
    public float attackTimer = 0f;
    public EnemyHealthBar healthBar;
    private Loot loot;

    public NavMeshAgent agent;   
    public FieldOfView fov;
    public Animator animator;

    //States
    public bool isWandering = true;
    public bool isAttacking = false;
    public bool isDead = false;

    public PlayerAttributes player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerAttributes>();
        healthBar = GameObject.Find("Canvas/Enemy Health Bar").GetComponent<EnemyHealthBar>();
        healthBar.SetMaxValue(maxHealth);
        health = maxHealth;
        healthBar.SetVisibility(false);
    }

    // Use this for initialization
    void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
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

    private void SwingTimer()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void SetAnimationState() 
    {
        if (animator != null)
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
    }

    public void TakeDamage(float damage) 
    {
        health -= damage;
        SetHealthBar();
        if (health <= 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("dead");
            }          
            isWandering = false;
            isAttacking = false;
            isDead = true;           
            loot.DropLoot();
            loot.DropGold();
            player.EarnExperience(experienceReward);
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void SetHealthBar() 
    {       
        healthBar.SetCurrentValue(health);
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


    public bool IsAtTarget()
    {
        if (fov.visibleTargets.Count > 0)
        {
            float dist = Vector3.Distance(fov.visibleTargets[0].transform.position, transform.position);
            if (dist <= attackRange)
            {
                return true;
            }
        }
        return false;
    }
}
