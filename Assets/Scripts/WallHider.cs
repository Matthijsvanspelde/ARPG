using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHider : MonoBehaviour
{
    private bool hideNeighbours = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WallDetector"))
        {
            hideNeighbours = true;
            gameObject.GetComponentInParent<Renderer>().enabled = false;
        }  
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WallDetector"))
        {
            hideNeighbours = true;
            gameObject.GetComponentInParent<Renderer>().enabled = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WallDetector"))
        {
            hideNeighbours = false;
            gameObject.GetComponentInParent<Renderer>().enabled = true;
        }
    }
}
