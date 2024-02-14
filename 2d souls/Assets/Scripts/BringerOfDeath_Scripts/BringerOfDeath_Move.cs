using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath_Move : StateMachineBehaviour
{
    public float speed = 1.0f;
<<<<<<< HEAD
    public float AttackRange = 5.0f;

    public float MagicAttackRange = 5.0f;

    public float SpellRange = 10.0f;


    Transform player;
    Rigidbody2D rb;
    BringerOfDeath boss;

    BringerOfDeath_Weapon weapon;
    float distance;

=======
    public float AttackRange = 3.0f;
    private Transform player;
    private Rigidbody2D rb;
    BringerOfDeath boss;
    BringerOfDeathWeapon weapon;
>>>>>>> andy

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
<<<<<<< HEAD
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       boss = animator.GetComponent<BringerOfDeath>();
       weapon = animator.GetComponent<BringerOfDeath_Weapon>();
=======
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<BringerOfDeath>();
        weapon = animator.GetComponent<BringerOfDeathWeapon>();
>>>>>>> andy
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.Flip();

        Vector2 target = new(player.position.x, rb.position.y);

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

<<<<<<< HEAD
        distance = Vector2.Distance(player.position, rb.position);

        
        if (distance > SpellRange && weapon.spellCooldownTimer < 0f)
        {
            animator.SetTrigger("CastSpell");
            weapon.spellCooldownTimer = weapon.spellCooldown;

        } else if (distance<MagicAttackRange && weapon.magicAttackCooldownTimer < 0f)
        {
            animator.SetTrigger("MagicAttack");
            weapon.magicAttackCooldownTimer = weapon.magicAttackCooldown;
        } else if (distance<AttackRange)
=======
        if (Vector2.Distance(player.position, rb.position) <= AttackRange && weapon.canAttack && boss.health > 0)
>>>>>>> andy
        {
            animator.SetTrigger("Attack");
        }

<<<<<<< HEAD
        

=======
        if (boss.health <= 0)
        {
            animator.SetTrigger("Death");
        }
>>>>>>> andy
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
<<<<<<< HEAD
       animator.ResetTrigger("Attack");
       animator.ResetTrigger("CastSpell");
       animator.ResetTrigger("MagicAttack");
=======
        animator.ResetTrigger("Attack");
>>>>>>> andy
    }
}
