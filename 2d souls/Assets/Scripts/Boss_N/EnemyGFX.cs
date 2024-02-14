using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    public float damageDuration = 0.2f;
    public Color damageColor = Color.red;
    public int dmgTaken;
    public int maxHealth = 200;
    public int health;
    public HealthBar healthbar;
    private const string targetSequence = "eal";
    private string currentSequence = "";
    public Animator mAnimator;
    public Animator plyrAnimator;
    private Color originalColor;
    public float attackRange = 10.2f;
    public float heroAttackRange = 10.2f;

    private float timer = 0f;
    private float interval = 0.5f;
    private float heroAttackTimer = 0f;
    private float heroAttackInterval = 0.5f;
    private float cheatTimer = 0f;
    private float cheatInterval = 3f;
    private float stageChangeTimer = 0f;
    private float stageChangeInterval = 0.5f;

    public HeroKnight hero;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponentInParent<CircleCollider2D>();
        originalColor = spriteRenderer.color;
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        dmgTaken = 10;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            // Get the most recently pressed key
            string keyPressed = Input.inputString;

            // Add the key to the current sequence
            currentSequence += keyPressed.ToLower();

            // Check if the current sequence matches the target sequence
            if (currentSequence.Equals(targetSequence))
            {
                // Do something when the target sequence is entered
                Debug.Log("Healing initiated!");
                hero.ResetHealth();

                cheatTimer += Time.deltaTime;
                if (cheatTimer >= cheatInterval)
                {
                    // Reset the current sequence for the next input
                    currentSequence = "";
                    cheatTimer = 0f;
                }
                
            }
        }

        Transform parentTransform = transform.parent;
        if (aiPath.desiredVelocity.x >= 0.01)
        {
            parentTransform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            parentTransform.localScale = new Vector3(1f, 1f, 1f);
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if ( hero.health == 30 )
            {
                //int randomNum = Random.Range(1, 5);
                //if (randomNum == 1)
                //    hero.ResetHealth();
            }
            // Calculate vector from enemy to player
            Vector3 directionToPlayer = player.transform.position - transform.position;

            // Check if player is within attack range and enemy is facing the player and if attack animation is playing
            AnimatorStateInfo stateInfo = plyrAnimator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo bossStateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
            if (directionToPlayer.magnitude <= attackRange &&
                Vector3.Dot(transform.forward, directionToPlayer.normalized) > 0.5f &&
                (stateInfo.IsName("Attack1") || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3")))
            {
                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    Debug.Log("damage taken");
                    healthbar.SetHealth(health);
                    takeDamage(dmgTaken);
                    timer = 0f;
                }
            }
            if(directionToPlayer.magnitude <= heroAttackRange &&
                Vector3.Dot(transform.forward, directionToPlayer.normalized) > 0.5f &&
                (bossStateInfo.IsName("Attack 1") || bossStateInfo.IsName("Attack 3") || bossStateInfo.IsName("Attack 4")) || (bossStateInfo.IsName("Attack 5") || bossStateInfo.IsName("Attack") || bossStateInfo.IsName("Attack Kick")))
            {
                heroAttackTimer += Time.deltaTime;
                if (heroAttackTimer >= heroAttackInterval)
                {
                    Debug.Log("Hero damage taken");
                    if (hero.health <= 10)
                    {
                        SceneManager.LoadScene("Start");
                    }
                    hero.TakeDamage(10);
                    heroAttackTimer = 0f;
                }
            }

        }
    }

    public void OnPlayerCollision()
    {
        Debug.Log("Player collided with enemy!");
    }

    public void takeDamage(int damage)
    {
        if (health <= 0)
        {
            aiPath.maxSpeed = 0.1f;
            mAnimator.SetTrigger("Dead");
        }
        if (health > 0)
        {
            health = health - damage;
        }
        if(health == 20)
        {
            int randomNum = Random.Range(1, 4);
            if (randomNum == 1)
                ResetHealth();
        }
        //else

        spriteRenderer.color = damageColor;
        Invoke("ResetColor", damageDuration);
    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }

    public void boss_n_Die()
    {
        SceneManager.LoadScene("Game");
    }

    public void ResetHealth()
    {
        Debug.Log("Unlucky XDXD");
        health = maxHealth;
        healthbar.SetHealth(maxHealth);
    }


}
