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
                target = null;
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
        if (IsAtTarget())
        {
            target.GetComponent<Interaction>().StartInteraction(gameObject);
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
