using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private FieldOfView fieldOfView;
    [SerializeField]
    private List<GameObject> enemies;

    private void Awake()
    {
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
        }
        fieldOfView = GetComponent<FieldOfView>();
    }

    void Update()
    {
        if (fieldOfView.FoundPlayer())
        {
            foreach (var enemy in enemies)
            {
                enemy.SetActive(true);
            }
        }       
    }
}
