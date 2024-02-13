using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath_Spell : MonoBehaviour
{

    public float duration = 2f;

    float startTime;



    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - startTime;
        if(elapsedTime > duration)
        {
            Die();
        }
    }

    void Die()
    {
        //Debug.Log("Spell End!");
        Destroy(gameObject);
    }
}
