using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public int damage = 5;
    public float knockbackPower = 5f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<CharacterHealth>() != null)
        {
            CharacterHealth health = col.GetComponent<CharacterHealth>();
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponentInParent<CharacterController>().score += health.takeDamage(damage);
            }
            else
            {
                health.takeDamage(damage);
            }
            if (col.tag != "Fireball" && col.tag != "Bats")
            {
                MonsterController movement = col.GetComponent<MonsterController>();
                movement.takeKnockback(knockbackPower, col.transform.position.x <= transform.position.x);
            }
        }
    }
}
