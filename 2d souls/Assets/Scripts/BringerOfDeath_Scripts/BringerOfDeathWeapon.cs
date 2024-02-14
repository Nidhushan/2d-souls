using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathWeapon : MonoBehaviour
{
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public float attackCooldown = 1.0f;

    [HideInInspector]
    public bool canAttack = true;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        if (player.position.x < pos.x)
        {
            pos += transform.right * attackOffset.x;
            pos += transform.up * attackOffset.y;
        }
        else
        {
            pos += transform.right * -attackOffset.x;
            pos += transform.up * -attackOffset.y;
        }

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            if (canAttack)
            {
                colInfo.GetComponent<HeroKnight>().TakeDamage(20);
                StartCoroutine(AttackCooldown());
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
