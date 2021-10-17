using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform projectileStart;

    private PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (playerAttack != null)
        {
            if (playerAttack.TargetRange.IsAtTarget(playerAttack.Target, playerAttack.AttackRange) && playerAttack.AttackTimer <= 0 && playerAttack.HasClicked)
            {
                playerAttack.Attack();
                GameObject projectile = Instantiate(this.projectile, projectileStart.position, Quaternion.identity);
                Projectile projectileLogic = projectile.GetComponent<Projectile>();
                projectileLogic.SetTarget(playerAttack.Target.gameObject.transform.position);
                var damage = playerAttack.playerAttributes.attributes.attackDamage + playerAttack.playerAttributes.AttackDamageBonus + ((float)(playerAttack.playerAttributes.attributes.strength + playerAttack.playerAttributes.StrengthBonus) / 100);
                projectileLogic.SetDamage(damage);
            }
        }
    }
}
