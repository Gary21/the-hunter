using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public int damage = 5;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<CharacterHealth>() != null)
        {
            CharacterHealth health = col.GetComponent<CharacterHealth>();
            health.takeDamage(damage);
        }
    }
}
