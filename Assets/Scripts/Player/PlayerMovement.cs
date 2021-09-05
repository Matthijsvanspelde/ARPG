using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetMoveDirection();
        Move();
    }

    private void Move()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Ground") && !EventSystem.current.IsPointerOverGameObject())
            {
                animator.SetBool("isWalking", true);
                agent.stoppingDistance = 0;
                agent.destination = hit.point;
            }
        }

        if (agent.velocity.magnitude == 0)
        {
            animator.SetBool("isWalking", false);
        }
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
