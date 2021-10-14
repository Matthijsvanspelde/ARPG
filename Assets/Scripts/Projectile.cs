using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField]
    private float speed = 1f;
    private Vector3 movementVector = Vector3.zero;
    private float damage;

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget() 
    {
        if (targetPosition != null)
        {
            transform.position += movementVector * Time.deltaTime;
            Vector3 relativePos = targetPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }       
    }

    public void SetTarget(Vector3 target) 
    {
        movementVector = (target - transform.position).normalized * speed;
        movementVector.y = 0;
    }

    public void SetDamage(float damage) 
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAttributes>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyAI>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
