using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath_Weapon : MonoBehaviour
{
    public float NormalAttackDamage = 10f;
    public Vector3 NormalAttackOffset;
    public float NormalAttackRange = 1f;

    public float MagicAttackDamage = 30f;
    public Vector3 MagicAttackOffset;
    public float MagicAttackRange = 3f;

    public LayerMask attackMask;

    public GameObject SpellPrefab;
    public Vector3 SpellSpawnOffset;

    Transform player;

    public void NormalAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * NormalAttackOffset.x;
        pos += transform.up * NormalAttackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, NormalAttackRange, attackMask);
        if(colInfo != null)
        {
            //Damage the player!
            Debug.Log("Normal attack hit on player!");
        }
    }

    public void MagicAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * MagicAttackOffset.x;
        pos += transform.up * MagicAttackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, MagicAttackRange, attackMask);
        if(colInfo != null)
        {
            //Damage the player!
            Debug.Log("Magic attack hit on player!");
        }
    }

    public void CastSpell()
    {
        Debug.Log("Cast Spell!");
        GameObject spell = Instantiate(SpellPrefab, player.position + SpellSpawnOffset,Quaternion.identity);
        //spell.transform.localScale()
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
