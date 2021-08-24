using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    private NavMeshAgent agent;
    private float timer;
    private EnemyFieldOfView fov;

    [SerializeField]
    private float stoppingDistance = 0.8f;

    private bool isWandering = true;
    private bool isAttacking = false;
    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<EnemyFieldOfView>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
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
        agent.destination = fov.visibleTargets[0].transform.position;        
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 RandomDirection = Random.insideUnitSphere * dist;
        RandomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(RandomDirection, out navHit, dist, layermask);
        return navHit.position;
    }

}
