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
        if (Input.GetKeyDown(KeyCode.G))
        {
            Place();
        }
    }

    public void Place() 
    {        
        Destroy(instantiatedPortal);
        instantiatedPortal = Instantiate(portalPrefab, new Vector3(transform.position.x, portalPrefab.transform.position.y, transform.position.z), Quaternion.identity);       
    }
}
