using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public float attackSpeed = 2;
    public float attackDamage = 2;

    [Header("AI")]
    public float wanderRadius;
    public float wanderTimer;
    public float attackRange = 0.8f;
    private float timer;
    public float attackTimer = 0f;

    public NavMeshAgent agent;   
    public FieldOfView fov;
    public Animator animator;
    [SerializeField]
    private Transform rig;

    //States
    public bool isWandering = true;
    public bool isAttacking = false;
    public bool isDead = false;

    public PlayerAttributes player;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerAttributes>();
    }

    // Use this for initialization
    void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
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
            rig.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (velocity < 0f)
        {
            rig.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
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
