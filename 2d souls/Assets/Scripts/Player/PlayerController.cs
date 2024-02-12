using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpForce = 5.0f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private int direction = 0;
    private bool shielding = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.velocity = new Vector2(-speed, _rigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }

        if (Input.GetMouseButton(1))
        {
            _rigidbody.velocity = Vector2.zero;
            shielding = true;
        }
        else
        {
            shielding = false;
        }

        if (_rigidbody.velocity.x < 0) direction = 1;
        if (_rigidbody.velocity.x > 0) direction = 0;

        if (!shielding)
        {
            _animator.SetFloat("speed", Mathf.Abs(_rigidbody.velocity.x));
            _spriteRenderer.flipX = (direction == 1);
        }
        else
        {
            _animator.SetTrigger("shield");
        }
    }
}
