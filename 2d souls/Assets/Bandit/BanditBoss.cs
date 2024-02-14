using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBoss : MonoBehaviour
{
    Transform player;
    private SpriteRenderer _spriteRenderer;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flip()
    {
        if (player.position.x > transform.position.x)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
}
