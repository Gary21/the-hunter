using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BatController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public float velocityxd;
    public Vector3 velocity;
    private float direction = 0f;
    private float directionY = 0f;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
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

        velocity = new Vector3(getSpeed(), getSpeedY(), 0);
        transform.position += velocity * (Time.deltaTime * moveSpeed);
    }
    
    float getSpeed()
    {
        if ((Mathf.Abs(velocity.x) >= moveSpeed || ( (direction < 0 && velocity.x < 0) || (direction > 0 && velocity.x > 0) )))
        {
            return direction * moveSpeed * Random.Range(0.95f, 1.06f);
        }

        else
        {
            return velocity.x + (velocityxd * direction * moveSpeed) * Random.Range(0.95f, 1.06f);
        }
    }
    
    float getSpeedY()
    {
        if ((Mathf.Abs(velocity.y) >= moveSpeed || ( (directionY < 0 && velocity.y < 0) || (directionY > 0 && velocity.y > 0) )))
        {
            return directionY * moveSpeed * Random.Range(0.95f, 1.06f);
        }

        else
        {
            return velocity.y + (velocityxd * directionY * moveSpeed) * Random.Range(0.95f, 1.06f);
        }
    }
}
