using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{


    public void StartInteraction(GameObject player) 
    {

        Debug.Log("Hello traveler!");
        SetSpriteDirection(player.transform.position.x);
    }

    public void NextDialogueNode() 
    { 
        
    }

    private void SetSpriteDirection(float playerXPosition)
    {
        float relativePosition = playerXPosition - transform.position.x;
        if (relativePosition > 0f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (relativePosition < 0f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }
}
