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
    public int maxHealth = 10;
    public int health;
    public Animator mAnimator;
    public Animator plyrAnimator;
    private Color originalColor;
    public float attackRange = 10.2f;
    private float timer = 0f;
    private float interval = 0.5f;

    private float stageChangeTimer = 0f;
    private float stageChangeInterval = 0.5f;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponentInParent<CircleCollider2D>();
        originalColor = spriteRenderer.color;
        health = maxHealth;
        dmgTaken = 1;
    }

    void Update()
    {
        if (health <= 0)
        {
            aiPath.maxSpeed = 0.1f;
            mAnimator.SetTrigger("Dead");
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
            // Calculate vector from enemy to player
            Vector3 directionToPlayer = player.transform.position - transform.position;
            
            // Check if player is within attack range and enemy is facing the player and if attack animation is playing
            AnimatorStateInfo stateInfo = plyrAnimator.GetCurrentAnimatorStateInfo(0);
            if (directionToPlayer.magnitude <= attackRange &&
                Vector3.Dot(transform.forward, directionToPlayer.normalized) > 0.5f &&
                (stateInfo.IsName("Attack1") || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3")))
            {
                // Apply damage to the player
                //PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                //if (playerHealth != null)
                //{
                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    Debug.Log("damage taken");
                    takeDamage(dmgTaken);
                    // Reset the timer
                    timer = 0f;
                }
                
                //}
            }
        }
    }

    public void OnPlayerCollision()
    {
        Debug.Log("Player collided with enemy!");
        //takeDamage(dmgTaken); 
    }

    public void takeDamage(int damage)
    {
        if (health > 0)
        {
            health = health - damage;
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
        Invoke("changeScene", 3f);
        Destroy(transform.parent.gameObject);
    }

    public void changeScene()
    {
        SceneManager.LoadScene("Game");
    }

}
