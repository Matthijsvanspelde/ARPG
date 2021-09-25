using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    private EnemyAI enemyAI;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform projectileStart;

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
            GameObject projectile = Instantiate(this.projectile, projectileStart.position, Quaternion.identity);
            Projectile projectileLogic = projectile.GetComponent<Projectile>();
            projectileLogic.SetTarget(enemyAI.player.gameObject.transform.position);
            projectileLogic.SetDamage(enemyAI.attackDamage);
            if (enemyAI.animator != null)
            {
                enemyAI.animator.SetTrigger("attack");
            }
        }
    }
}
