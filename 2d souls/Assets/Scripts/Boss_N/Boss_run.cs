using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_run : StateMachineBehaviour
{

    Transform player;
    Rigidbody2D rb;
    public float attackRange = 3f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponentInParent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(player.position, rb.position) <= attackRange)
        {

            animator.SetTrigger("Attack start");

            //int randomNum = Random.Range(1, 3);
            //Debug.Log(randomNum);
            //switch (randomNum)
            //{
            //    case 1:
            //        Debug.Log("Attack on update");
            //        animator.SetTrigger("Attack");
            //        break;

            //    case 2:
            //        Debug.Log("Attack 1 on update");
            //        animator.SetTrigger("Attack1");
            //        break;

            //    default:
            //        Debug.Log("Not working......");
            //        break;
            //}


        }
        //else
        //{
        //    animator.SetTrigger("Player Away");
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack start");
        //animator.ResetTrigger("Player Away");
    }
}
