using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    public float knockBackPower;
    public CharacterHealth health;
    public CharacterController movement;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            movement.takeKnockback(knockBackPower, col.transform.position.x <= transform.position.x);
            health.takeDamage(damage);
        }
    }
}
