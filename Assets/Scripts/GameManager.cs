using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform spawnPoint;

    private void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer() 
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        if (player != null)
        {
            return;
        }
        else
        {
            Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
