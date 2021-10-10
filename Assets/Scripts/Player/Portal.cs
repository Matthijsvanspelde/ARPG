using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private GameObject portalPrefab;
    private GameObject instantiatedPortal;
    [SerializeField]
    private Transform spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Place();
    }

    private void Place() 
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Destroy(instantiatedPortal);
            instantiatedPortal = Instantiate(portalPrefab, new Vector3(transform.position.x, portalPrefab.transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}
