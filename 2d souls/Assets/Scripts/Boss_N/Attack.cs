using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{

    Transform player;
    Rigidbody2D rb;
    public float attackRange = 3f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponentInParent<Rigidbody2D>();
        int randomNum = Random.Range(1, 7);
        Debug.Log(randomNum);
        switch (randomNum)
        {
            case 1:
                Debug.Log("Attack on update");
                animator.SetTrigger("Attack");
                break;

            case 2:
                Debug.Log("Attack 1 on update");
                animator.SetTrigger("Attack 1");
                break;

            case 3:
                Debug.Log("Attack on update");
                animator.SetTrigger("Attack 3");
                break;

            case 4:
                Debug.Log("Attack 1 on update");
                animator.SetTrigger("Attack 4");
                break;

            case 5:
                Debug.Log("Attack on update");
                animator.SetTrigger("Attack 5");
                break;

            case 6:
                Debug.Log("Attack 1 on update");
                animator.SetTrigger("Attack Kick");
                break;

            default:
                Debug.Log("Not working......");
                break;
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack 1");
        animator.ResetTrigger("Attack 3");
        animator.ResetTrigger("Attack 4");
        animator.ResetTrigger("Attack 5");
        animator.ResetTrigger("Attack Kick");
        animator.ResetTrigger("Player Away");
    }

}
