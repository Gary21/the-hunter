using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100;
    public float baseHpRegen = 0;
    public float hpRegenMultiplier = 1.0f;
    [SerializeReference] private FlashEffect flashEffect;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        hpRegenHandler();
    }

    void hpRegenHandler()
    {
        currentHealth += baseHpRegen * hpRegenMultiplier * Time.deltaTime;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void takeDamage(int damage)
    {
        flashEffect.Flash();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
