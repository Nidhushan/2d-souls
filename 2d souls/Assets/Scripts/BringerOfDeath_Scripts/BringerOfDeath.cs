using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BringerOfDeath : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public EnemyHealthbar enemyHealthbar;

    Transform player;
    private SpriteRenderer _spriteRenderer;
    Animator _animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void Flip()
    {
        if (player.position.x > transform.position.x)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            enemyHealthbar.UpdateHealthbar(health, maxHealth);
            _animator.SetTrigger("Hurt");
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
