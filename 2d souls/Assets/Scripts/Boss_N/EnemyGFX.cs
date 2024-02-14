using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
    public Animator animator;
    private Color originalColor;


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
        Transform parentTransform = transform.parent;
        if (aiPath.desiredVelocity.x >= 0.01)
        {
            parentTransform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            parentTransform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void OnPlayerCollision()
    {
        Debug.Log("Player collided with enemy!");
        takeDamage(dmgTaken);
    }

    public void takeDamage(int damage)
    {
        if (health <= 0)
        {
            animator.SetTrigger("Dead");
        }
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

}
