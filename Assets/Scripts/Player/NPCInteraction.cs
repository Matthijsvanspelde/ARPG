using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField]
    private float stoppingDistance = 0.8f;
    private GameObject target;
    private NavMeshAgent agent;
    private bool isInteracting = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Interact();
        SetTarget();
        WalkToTarget();
    }

    private void SetTarget()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("NPC") && !EventSystem.current.IsPointerOverGameObject())
            {
                target = hit.transform.gameObject;
                agent.stoppingDistance = stoppingDistance;
            }
            else
            {
                if (target != null)
                {
                    target.GetComponent<Dialogue>().EndDialogue();
                }               
                target = null;
                isInteracting = false;
            }
        }
    }

    private void WalkToTarget()
    {
        if (target != null)
        {
            agent.destination = target.transform.position;
        }
    }

    private void Interact() 
    {
        if (IsAtTarget() && !isInteracting)
        {
            isInteracting = true;
            target.GetComponent<Dialogue>().StartDialogue(gameObject);
        }
    }

    private bool IsAtTarget()
    {
        if (target != null)
        {
            
            float dist = Vector3.Distance(target.transform.position, transform.position);
            Debug.Log(dist);
            if (dist <= stoppingDistance + 0.1)
            {
                return true;
            }
        }
        return false;
    }
}
