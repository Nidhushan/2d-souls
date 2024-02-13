using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    CircleCollider2D circleCollider;
    
    void Start()
    {
        // Get the SpriteRenderer component attached to the same GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponentInParent<BoxCollider2D>();
        circleCollider = GetComponentInParent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            //boxCollider.offset = new Vector3(-0.93f, 0.38f);
            circleCollider.offset = new Vector3(-1f, 0.30f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            //boxCollider.offset = new Vector3(0.15f, 0.44f);
            circleCollider.offset = new Vector3(0f, 0.3f);
        }
    }
}
