using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    private NavMeshAgent agent;
    private float attackRange;
    public PlayerAttributes playerAttributes;
    private GameObject target;
    private float attackTimer = 0f;
    private bool hasClicked = false;
    private Animator animator;
    public EnemyAI enemyAI;
    private TargetRange targetRange;

    public float AttackTimer { get => attackTimer; private set => attackTimer = value; }
    public bool HasClicked { get => hasClicked; private set => hasClicked = value; }
    public GameObject Target { get => target; private set => target = value; }
    public TargetRange TargetRange { get => targetRange; private set => targetRange = value; }
    public float AttackRange { get => attackRange; private set => attackRange = value; }


    // Start is called before the first frame update
    void Start()
    {
        targetRange = GetComponent<TargetRange>();
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();   
    }

    // Update is called once per frame
    void Update()
    {
        TargetEnemy();
        SwingTimer();
        WalkToTarget();
    }

    private void TargetEnemy() 
    {
        RaycastHit hit;
        int mask = 1 << 9;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {          
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Enemy") && !EventSystem.current.IsPointerOverGameObject())
            {
                animator.SetBool("isWalking", true);
                target = hit.transform.gameObject;
                enemyAI = hit.transform.gameObject.GetComponent<EnemyAI>();
                attackRange = playerAttributes.attributes.attackRange + playerAttributes.AttackRangeBonus;
                agent.stoppingDistance = attackRange;
                SetSpriteDirection();
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
            if (targetRange.IsAtTarget(target, attackRange))
            {
                agent.destination = transform.position;
            }
        }
    }

    public void Attack() 
    {
        hasClicked = false;        
        animator.SetTrigger("swing");
        attackTimer = playerAttributes.attributes.attackSpeed + playerAttributes.AttackSpeedBonus - ((float)(playerAttributes.attributes.dexterity + playerAttributes.DexterityBonus) / 100);
        agent.destination = transform.position;                           
    }

    private void SwingTimer() 
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void SetSpriteDirection()
    {
        float relativePosition = target.transform.position.x - transform.position.x;
        if (relativePosition > 0f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (relativePosition < 0f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }
}
