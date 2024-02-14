using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Timeline;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 12.0f;
    [SerializeField] GameObject m_slideDust;
    public int health = 100;
    public int maxHealth = 100;
    public int stamina = 100;
    public HealthBar healthbar;
    public StaminaBar staminabar;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private readonly bool       m_isWallSliding = false;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private readonly float      m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;
    private bool                m_CanDamage = true;
    private bool                takingDamage = false;
    private bool isAttacking = false;
    private bool alreadyDead = false;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    private List<Coroutine> staminaCoroutines = new();
    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        healthbar.SetMaxHealth(maxHealth);
        staminabar.SetMaxStamina(stamina);
    }

    // Update is called once per frame
    void Update ()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (m_rollCurrentTime > m_rollDuration)
        {
            m_rolling = false;
        }

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling && !takingDamage && !isAttacking)
        {
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        if (Input.GetKeyDown(KeyCode.V) && m_CanDamage && !alreadyDead)
        {
            m_body2d.velocity = Vector2.zero;
            takingDamage = true;
            StartCoroutine(TakingDamage());
            m_body2d.AddForce(new Vector2(-m_facingDirection * 100.0f, 0));
            m_animator.SetTrigger("Hurt");
        }
        //Attack
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            Vector3 pos = transform.position;
            pos += transform.right * attackOffset.x;
            pos += transform.up * attackOffset.y;

            Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
            if (colInfo != null)
            {
                //if (colInfo.gameObject.CompareTag("Death"))
                //    colInfo.GetComponent<BringerOfDeath>().TakeDamage(20);
                //else if (colInfo.gameObject.CompareTag("Bandit"))
                //    colInfo.GetComponent<>
                colInfo.GetComponent<BringerOfDeath>().TakeDamage(20);
            }

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }
        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }
        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);
        // Roll
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !m_rolling && !m_isWallSliding && stamina >= 20)
        {
            m_rolling = true;
            m_CanDamage = false;
            StartCoroutine(Dodge());
            stamina -= 20;
            staminabar.SetStamina(stamina);
            foreach (var coroutine in staminaCoroutines)
            {
                StopCoroutine(coroutine);
            }
            staminaCoroutines.Add(StartCoroutine(StaminaIncrease()));
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }
        //Jump
        else if (Input.GetKeyDown(KeyCode.Space) && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon && !isAttacking)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }
        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }

        if (health <= 0 && !alreadyDead)
        {
            alreadyDead = true;
            m_animator.SetTrigger("Death");
            StartCoroutine(Die());
        }
    }

    public void TakeDamage(int dmg)
    {
        if (m_CanDamage)
        {
            m_body2d.velocity = Vector2.zero;
            takingDamage = true;
            health -= dmg;
            healthbar.SetHealth(health);
            StartCoroutine(TakingDamage());
            m_body2d.AddForce(new Vector2(-m_facingDirection * 100.0f, 0));
            m_animator.SetTrigger("Hurt");
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
        healthbar.SetHealth(maxHealth);
    }

    IEnumerator TakingDamage()
    {
        yield return new WaitForSeconds(0.1f);
        takingDamage = false;
    }

    IEnumerator Dodge()
    {
        yield return new WaitForSeconds(0.5f);
        m_CanDamage = true;
    }

    IEnumerator StaminaIncrease()
    {
        while (stamina < 100)
        {
            stamina++;
            staminabar.SetStamina(stamina);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("End");
    }
}
