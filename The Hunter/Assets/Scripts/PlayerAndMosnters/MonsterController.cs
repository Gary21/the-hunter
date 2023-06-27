using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int moveSpeed;
    public float velocity;
    private Rigidbody2D monster;
    private float direction = 0f;
    private float directionY = 0f;
    private Transform playerTransform;
    private bool isLookingRight = true;
    float kbForce;
    float kbCounter;
    bool knockFromRight;
    private Animator monsterAnimator;
    
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] private float rangeInterval;
    public float intervalMultiplier;
    
    void Start()
    {
        monster = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        monsterAnimator = GetComponent<Animator>();
        if (gameObject.tag.Equals("Wizards"))
        {
            StartCoroutine(RangeAttack(rangeInterval, projectilePrefab));
        }
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
        
        if (transform.position.y > playerTransform.position.y)
        {
            directionY = -1;
        }
        
        if (transform.position.y < playerTransform.position.y)
        {
            directionY = 1;
        }
        
        if (gameObject.tag.Equals("Ghouls"))
        {
            if (kbCounter <= 0)
            {
                MoveHandler();
            }
            else
            {
                KnockbackHandler();
            }
        }

        else if (gameObject.tag.Equals("Wizards"))
        {
            KnockbackHandler();
            if (kbCounter <= 0)
            {
                monster.velocity = new Vector2(0, monster.velocity.y);
            }
        }
        
        else if (gameObject.tag.Equals("Bats"))
        {
            
        }
    }
    
    void MoveHandler()
    {
        if (direction > 0f)
        {
            monster.velocity = new Vector2(getSpeed(), monster.velocity.y);
            RotateMonster(false);
        }
        else if (direction < 0f)
        {
            monster.velocity = new Vector2(getSpeed(), monster.velocity.y);
            RotateMonster(true);
        }
        else
        {
            monster.velocity = new Vector2(0, monster.velocity.y);
        }
    }

    float getSpeed()
    {
        if ((Mathf.Abs(monster.velocity.x) >= moveSpeed || ( (direction < 0 && monster.velocity.x < 0) || (direction > 0 && monster.velocity.x > 0) )) && kbCounter <= 0)
        {
            return direction * moveSpeed * Random.Range(0.95f, 1.06f);
        }

        else
        {
            return monster.velocity.x + (velocity * direction * moveSpeed) * Random.Range(0.95f, 1.06f);
        }
    }
    
    float getSpeedY()
    {
        if ((Mathf.Abs(monster.velocity.y) >= moveSpeed || ( (directionY < 0 && monster.velocity.y < 0) || (directionY > 0 && monster.velocity.y > 0) )) && kbCounter <= 0)
        {
            return directionY * moveSpeed * Random.Range(0.95f, 1.06f);
        }

        else
        {
            return monster.velocity.y + (velocity * directionY * moveSpeed) * Random.Range(0.95f, 1.06f);
        }
    }
    
    void KnockbackHandler()
    {
        if (knockFromRight)
        {
            monster.velocity = new Vector2(-kbForce * Random.Range(0.95f, 1.06f), 0.2f * kbForce);
        }
        else
        {
            monster.velocity = new Vector2(kbForce * Random.Range(0.95f, 1.06f), 0.2f * kbForce);
        }

        kbCounter -= Time.deltaTime;
    }
    
    void RotateMonster(bool toRight)
    {
        if (toRight != isLookingRight)
        {
            isLookingRight = !isLookingRight;
            transform.Rotate(0,180,0);
        }
    }

    void BatsMovement()
    {
        if (direction > 0f)
        {
            monster.velocity = new Vector2(getSpeed(), getSpeedY());
            RotateMonster(false);
        }
        else if (direction < 0f)
        {
            monster.velocity = new Vector2(getSpeed(), getSpeedY());
            RotateMonster(true);
        }
        else
        {
            monster.velocity = new Vector2(0, monster.velocity.y);
        }
    }

    private IEnumerator RangeAttack(float interval, GameObject projectile)
    {
        yield return new WaitForSeconds(interval * intervalMultiplier);
        monsterAnimator.Play("MageAttack");
        StartCoroutine(RangeAttack(interval, projectile));
    }

    public Vector3 getVelocity()
    {
        var vector = playerTransform.position - transform.position;
        vector.Normalize();
        return vector;
    }

    public void takeKnockback(float force, bool fromRight, float time = 0.25f)
    {
        knockFromRight = fromRight;
        kbForce = force;
        kbCounter = time;
    }
}
