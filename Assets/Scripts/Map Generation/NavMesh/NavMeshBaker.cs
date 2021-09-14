using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            navMeshSurface.BuildNavMesh();
        }
    }
}

