using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int moveSpeed;
    public float velocity;
    private Rigidbody2D monster;
    private float direction = 0f;
    public Transform playerTransform;
    float kbForce;
    float kbCounter;
    bool knockFromRight;
    
    void Start()
    {
        monster = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            direction = -1;
        }
        
        if (transform.position.x < playerTransform.position.x)
        {
            direction = 1;
        }

        if (kbCounter <= 0)
        {
            MoveHandler();
        }
        else
        {
            KnockbackHandler();
        }
    }
    
    void MoveHandler()
    {
        if (direction > 0f)
        {
            monster.velocity = new Vector2(getSpeed(), monster.velocity.y);
        }
        else if (direction < 0f)
        {
            monster.velocity = new Vector2(getSpeed(), monster.velocity.y);
        }
        else
        {
            monster.velocity = new Vector2(0, monster.velocity.y);
        }
    }

    float getSpeed()
    {
        if (Mathf.Abs(monster.velocity.x) >= moveSpeed && ( (direction < 0 && monster.velocity.x < 0) || (direction > 0 && monster.velocity.x > 0) ))
        {
            return direction * moveSpeed;
        }

        else
        {
            return monster.velocity.x + (velocity * direction * moveSpeed);
        }
    }
    
    void KnockbackHandler()
    {
        if (knockFromRight)
        {
            monster.velocity = new Vector2(-kbForce, 0.2f * kbForce);
        }
        else
        {
            monster.velocity = new Vector2(kbForce, 0.2f * kbForce);
        }

        kbCounter -= Time.deltaTime;
    }
    
    public void takeKnockback(float force, bool fromRight, float time = 0.25f)
    {
        knockFromRight = fromRight;
        kbForce = force;
        kbCounter = time;
    }
}
