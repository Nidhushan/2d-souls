using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss_run : StateMachineBehaviour
{

    Transform player;
    Rigidbody2D rb;
    public float attackRange = 3f;
    public float desiredSpeed = 3f;
    AIPath aiPath;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponentInParent<Rigidbody2D>();
        aiPath = animator.GetComponentInParent<AIPath>();
        aiPath.maxSpeed = desiredSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(player.position, rb.position) < attackRange)
        {

            animator.SetTrigger("Attack start");

        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack start");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead") && animator.GetBool("Dead"))
        {
            aiPath.maxSpeed = 0.1f;
            animator.Play("Dead");
        }
    }
}
