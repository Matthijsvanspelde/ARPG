using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 20;

    private void Update()
    {
        CheckIfDead();
    }

    private void CheckIfDead() 
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
