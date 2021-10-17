using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
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
                var damage = playerAttack.playerAttributes.attributes.attackDamage + playerAttack.playerAttributes.AttackDamageBonus + ((float)(playerAttack.playerAttributes.attributes.strength + playerAttack.playerAttributes.StrengthBonus) / 100);
                playerAttack.enemyAI.TakeDamage(damage);
            }
        }        
    }
}
