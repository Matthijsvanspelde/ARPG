using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private float stoppingDistance = 0.8f;
    [SerializeField]
    PlayerAttributes playerAttributes;
    private GameObject target;
    private float attackTimer = 0f;
    private bool hasClicked = false;
    private Animator animator;
    private EnemyAI enemyAI;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();   
    }

    // Update is called once per frame
    void Update()
    {
        TargetEnemy();
        SwingTimer();
        Attack();
        WalkToTarget();
    }

    private void TargetEnemy() 
    {
        RaycastHit hit;
        int mask = 1 << 9;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask) && hit.collider.CompareTag("Enemy") && !EventSystem.current.IsPointerOverGameObject())
            {
                animator.SetBool("isWalking", true);
                target = hit.transform.gameObject;
                enemyAI = hit.transform.gameObject.GetComponent<EnemyAI>();
                agent.stoppingDistance = stoppingDistance;
                
                enemyAI.SetHealthBar();
                enemyAI.healthBar.SetNameTag(target.name);
                enemyAI.healthBar.SetVisibility(true);
                if (attackTimer <= 0)
                {
                    hasClicked = true;
                }
            }
            else 
            {
                if (target != null)
                {
                    enemyAI.healthBar.SetVisibility(false);
                }               
                target = null;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask) && hit.collider.CompareTag("Enemy"))
            {
                hasClicked = true;
            }            
        }
    }

    private void WalkToTarget() 
    {
        if (target != null)
        {
            agent.destination = target.transform.position;
            if (IsAtTarget())
            {
                agent.destination = transform.position;
            }
        }
    }

    private void Attack() 
    {
        
        if (IsAtTarget() && attackTimer <= 0 && hasClicked)
        {
            hasClicked = false;
            var damage = playerAttributes.attributes.attackDamage + playerAttributes.AttackDamageBonus + ((float)(playerAttributes.attributes.strength + playerAttributes.StrengthBonus) / 100);
            enemyAI.TakeDamage(damage);
            animator.SetTrigger("swing");
            attackTimer = playerAttributes.attributes.attackSpeed + playerAttributes.AttackSpeedBonus - ((float)(playerAttributes.attributes.dexterity + playerAttributes.DexterityBonus) / 100);
            agent.destination = transform.position;
            if (enemyAI.isDead)
            {
                enemyAI.healthBar.SetVisibility(false);
            }                     
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
            if (dist <= stoppingDistance)
            {
                return true;
            }
        }
        return false;
    }
}
