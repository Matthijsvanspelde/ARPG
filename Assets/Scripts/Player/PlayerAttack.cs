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
    public HittableHealth hittable;
    private TargetRange targetRange;
    private AudioSource audioSource;

    public float AttackTimer { get => attackTimer; private set => attackTimer = value; }
    public bool HasClicked { get => hasClicked; private set => hasClicked = value; }
    public GameObject Target { get => target; private set => target = value; }
    public TargetRange TargetRange { get => targetRange; private set => targetRange = value; }
    public float AttackRange { get => attackRange; private set => attackRange = value; }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Hittable") && !EventSystem.current.IsPointerOverGameObject())
            {
                target = hit.transform.gameObject;
                hittable = hit.transform.gameObject.GetComponent<HittableHealth>();
                attackRange = playerAttributes.attributes.attackRange + playerAttributes.AttackRangeBonus;
                agent.stoppingDistance = attackRange;
                SetSpriteDirection();
                hittable.SetHealthBar();
                hittable.healthBar.SetNameTag(target.name);
                hittable.healthBar.SetVisibility(true);
                if (attackTimer <= 0)
                {
                    hasClicked = true;
                }
            }
            else
            {
                if (target != null)
                {
                    hittable.healthBar.SetVisibility(false);
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
        attackTimer = playerAttributes.attributes.attackSpeed - playerAttributes.AttackSpeedBonus - ((float)(playerAttributes.attributes.dexterity + playerAttributes.DexterityBonus) / 100);
        animator.SetTrigger("swing");
        audioSource.clip = GetComponentInChildren<WeaponItem>().attackSound;
        audioSource.Play();
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
