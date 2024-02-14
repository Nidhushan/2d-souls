using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath_Move : StateMachineBehaviour
{
    public float speed = 1.0f;
    public float AttackRange = 3.0f;
    private Transform player;
    private Rigidbody2D rb;
    private bool isDead = false;
    BringerOfDeath boss;
    BringerOfDeathWeapon weapon;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<BringerOfDeath>();
        weapon = animator.GetComponent<BringerOfDeathWeapon>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isDead)
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
                animator.SetTrigger("Death");
                isDead = true;
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Death");
    }
}
