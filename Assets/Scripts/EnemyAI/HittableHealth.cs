using UnityEngine;

public class HittableHealth : MonoBehaviour
{
    public EnemyHealthBar healthBar;
    public float health;
    public float maxHealth = 8;
    private DamageNumber damageNumber;
    private HitFeedback hitFeedback;
    private GameObject player;
    private Animator animator;
    private Loot loot;
    [SerializeField]
    private int experienceReward = 5;
    private EnemyAI enemyAI;

    private void Awake()
    {
        healthBar = GameObject.Find("Canvas/Enemy Health Bar").GetComponent<EnemyHealthBar>();
        healthBar.SetMaxValue(maxHealth);
        health = maxHealth;
        healthBar.SetVisibility(false);
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        loot = GetComponent<Loot>();
        damageNumber = GetComponentInChildren<DamageNumber>();
        hitFeedback = GetComponent<HitFeedback>();
        player = GameObject.Find("Player");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hitFeedback.Flash();
        damageNumber.Create(damage);
        SetHealthBar();
        if (health <= 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("dead");
            }
            if (enemyAI != null)
            {
                enemyAI.isWandering = false;
                enemyAI.isAttacking = false;
                enemyAI.isDead = true;
                CheckQuestGoal();
            }
            GetComponent<BoxCollider>().enabled = false;
            loot.DropLoot();
            loot.DropGold();
            player.GetComponent<PlayerAttributes>().EarnExperience(experienceReward);
            healthBar.SetVisibility(false);
        }
    }

    private void CheckQuestGoal() 
    {
        QuestLog questLog = player.GetComponent<QuestLog>();
        foreach (var quest in questLog.quests)
        {
            if (quest.questInfo.isActive && !quest.questInfo.isCompleted)
            {
                quest.questInfo.goal.EnemyKilled(enemyAI.enemyType);
                if (quest.questInfo.goal.isReached())
                {
                    player.GetComponent<PlayerAttributes>().EarnExperience(quest.questInfo.experienceReward);
                    quest.Complete();
                }
                questLog.UpdateQuestLog(quest);
            }           
        }
       
    }

    public void SetHealthBar()
    {
        healthBar.SetMaxValue(maxHealth);
        healthBar.SetCurrentValue(health);
    }
}
