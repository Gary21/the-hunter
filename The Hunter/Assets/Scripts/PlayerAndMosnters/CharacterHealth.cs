using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100;
    public float baseHpRegen = 0;
    public float hpRegenMultiplier = 1.0f;
    private Animator playerAnimator;
    [SerializeReference] private FlashEffect flashEffect;
    private float buffTimer = 0.0f;
    void Start()
    {
        currentHealth = maxHealth;
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hpRegenHandler();
        buffTimer -= Time.deltaTime;
    }

    void hpRegenHandler()
    {
        var buffMultiplier = buffTimer > 0 ? 2 : 1;
        currentHealth += baseHpRegen * hpRegenMultiplier * buffMultiplier * Time.deltaTime;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public int takeDamage(int damage)
    {
        flashEffect.Flash();
        currentHealth -= damage;
        if (gameObject.tag == "Player" && currentHealth <= 0)
        {
            //call death animation
            //call death screen
            
            Destroy(gameObject);
            FindObjectOfType<GameOverMenu>().displayEndingScreen();
            //SceneManager.LoadScene(1);
        }
        else if (gameObject.tag == "Fireball")
        {
            Destroy(gameObject);
        }
        else if (currentHealth <= 0)
        {
            playerAnimator.SetBool("isDying", true);
            return 10;
        }

        return 0;
    }
    
    public void takeBuff()
    {
        currentHealth = maxHealth;
        buffTimer = 15.0f;
    }
}
