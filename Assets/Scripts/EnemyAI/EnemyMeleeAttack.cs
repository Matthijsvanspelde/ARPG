using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private EnemyAI enemyAI;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        if (enemyAI != null && enemyAI.isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {

        enemyAI.agent.stoppingDistance = enemyAI.attackRange;
        if (enemyAI.fov.visibleTargets.Count > 0)
        {
            enemyAI.agent.destination = enemyAI.fov.visibleTargets[0].transform.position;
        }
        else
        {
            enemyAI.isWandering = true;
            enemyAI.isAttacking = false;
        }
        if (enemyAI.IsAtTarget() && enemyAI.isAttacking && enemyAI.attackTimer <= 0)
        {
            enemyAI.attackTimer = enemyAI.attackSpeed;
            enemyAI.player.TakeDamage(enemyAI.attackDamage);
            if (enemyAI.animator != null)
            {
                enemyAI.animator.SetTrigger("attack");
            }
        }
    }
}
