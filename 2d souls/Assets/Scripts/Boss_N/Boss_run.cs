using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_run : StateMachineBehaviour
{
    public float AttackRange = 2.0f;
    Transform player;
    Rigidbody2D rb;
    public float attackRange = 3f;
    public float speed = 2.5f;
    BringerOfDeath boss;
    BringerOfDeathWeapon weapon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponentInParent<Rigidbody2D>();
        boss = animator.GetComponentInParent<BringerOfDeath>();
        weapon = animator.GetComponent<BringerOfDeathWeapon>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.Flip();
        Vector2 target = new(player.position.x, rb.position.y);

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= AttackRange && weapon.canAttack && boss.health > 0)
        {
            animator.SetTrigger("Attack");
        }

        if (boss.health <= 0)
        {
            animator.SetTrigger("Die");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
