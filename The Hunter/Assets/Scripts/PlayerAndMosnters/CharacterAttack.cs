using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public int damage = 5;
    public float knockbackPower = 5f;
    private float buffTimer = 0.0f;

    private void Update()
    {
        buffTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var currentDamage = buffTimer > 0 ? damage * 2 : damage;
        if (col.GetComponent<CharacterHealth>() != null)
        {
            CharacterHealth health = col.GetComponent<CharacterHealth>();
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponentInParent<CharacterController>().score += health.takeDamage(currentDamage);
            }
            else
            {
                health.takeDamage(currentDamage);
            }
            if (col.tag != "Fireball" && col.tag != "Bats")
            {
                MonsterController movement = col.GetComponent<MonsterController>();
                movement.takeKnockback(knockbackPower, col.transform.position.x <= transform.position.x);
            }
        }
    }
    
    public void takeBuff()
    {
        buffTimer = 15.0f;
    }
}
