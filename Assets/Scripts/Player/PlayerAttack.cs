using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private float stoppingDistance = 0.8f;
    [SerializeField]
    PlayerStatsScriptableObject playerData;
    private GameObject target;
    private float attackTimer = 0f;
    private bool hasClicked = false;
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   
    }

    // Update is called once per frame
    void Update()
    {
        TargetEnemy();
        SwingTimer();
        Attack();       
    }

    private void TargetEnemy() 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {           
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Enemy"))
            {                
                target = hit.transform.gameObject;
                agent.stoppingDistance = stoppingDistance;
                agent.destination = hit.point;
                hasClicked = true;
            }
            else 
            {
                target = null;
            }
        }
    }

    private void Attack() 
    {
        if (IsAtTarget() && attackTimer <= 0 && hasClicked)
        {
            hasClicked = false;           
            target.GetComponent<EnemyController>().health -= playerData.baseAttackDamage;
            Debug.Log(target.GetComponent<EnemyController>().health);
            attackTimer = playerData.attackSpeed;            
        }
    }

    private void SwingTimer() 
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private bool IsAtTarget() 
    {
        if (target != null)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            if (dist <= 1.5f)
            {
                return true;
            }
        }
        return false;
    }
}
