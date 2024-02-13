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
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponentInParent<CircleCollider2D>();
        
    }

    void Update()
    {
        Transform parentTransform = transform.parent;
        if (aiPath.desiredVelocity.x >= 0.01)
        {
            parentTransform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            parentTransform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

}
