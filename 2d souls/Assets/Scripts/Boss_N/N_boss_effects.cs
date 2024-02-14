using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_boss_effects : MonoBehaviour
{
    public EnemyGFX enemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.OnPlayerCollision();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
