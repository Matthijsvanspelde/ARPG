using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private static GameObject[] existingObject;

    void Start()
    {
        existingObject = GameObject.FindGameObjectsWithTag(gameObject.tag);
        if (existingObject.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }

}
