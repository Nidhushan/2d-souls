using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath_Move : StateMachineBehaviour
{
    public float speed = 1.0f;
    public float AttackRange = 5.0f;

    public float SpellRange = 10.0f;

    public float SpellCoolDown = 3.0f;

    float SpellCoolDownTimer = 0f;

    Transform player;
    Rigidbody2D rb;
    BringerOfDeath boss;
    float distance;


    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       boss = animator.GetComponent<BringerOfDeath>();
       SpellCoolDownTimer = SpellCoolDown;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        distance = Vector2.Distance(player.position, rb.position);

        SpellCoolDownTimer -= Time.fixedDeltaTime;
        if (distance > SpellRange && SpellCoolDownTimer<0f)
        {
            animator.SetTrigger("CastSpell");
            SpellCoolDownTimer = SpellCoolDown;

        } else if (distance<AttackRange)
        {
            animator.SetTrigger("Attack");
        }

        

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Attack");
       animator.ResetTrigger("CastSpell");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
