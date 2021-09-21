using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TileEndInteraction : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agent;
    [SerializeField]
    private TileGenerator tileGenerator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        WalkToEndTile();
        GoToNextLevel();
    }

    private void GoToNextLevel()
    {
        if (IsAtTarget())
        {
            
            tileGenerator.SetTileSetData();
        }
    }

    private void WalkToEndTile()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Stairs") && !EventSystem.current.IsPointerOverGameObject())
            {
                target = hit.collider.gameObject;
                agent.destination = target.transform.position;
            }
            else
            {
                target = null;
            }
        }
    }

    private bool IsAtTarget()
    {
        if (target != null)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            Debug.Log(dist);
            if (dist <= 1.5)
            {
                return true;
            }
        }
        return false;
    }
}
