using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public void StartInteraction(GameObject player) 
    {
        Debug.Log("Hello traveler!");
        float dist = Vector3.Distance(player.transform.position, transform.position);

        Debug.Log(dist);
    }
}
