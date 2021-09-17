using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisibility : MonoBehaviour
{
    private FieldOfView fieldOfView;
    [SerializeField]
    private GameObject rig;

    private void Awake()
    {
        fieldOfView = GetComponent<FieldOfView>();
    }

    void Update()
    {
        if (fieldOfView.FoundPlayer())
        {
            rig.SetActive(true);
        }
        else
        {
            rig.SetActive(false);
        }
    }
}
