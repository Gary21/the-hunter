using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 5f;
    private float direction = 0f;
    private Rigidbody2D player;
    private GameObject attackArea = default;
    public float attackCooldown = 100.25f;
    private float attackTimer = 0.0f;
    private bool isLookingRight = true;
    private bool isMoving = false;
    private bool isAttacking = false;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private bool doubleJump;
    public int score = 0;

    float kbForce;
    float kbCounter;
    bool knockFromRight;
    
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.Find("AttackArea").gameObject;
        player = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        if (kbCounter <= 0)
        {
            MoveHandler();
        }
        else
        {
            KnockbackHandler();
        }
        
        JumpHandler();
        AttackHandler();

        isMoving = direction != 0;

        playerAnimator.SetBool("isMoving", isMoving);
        playerAnimator.SetBool("isOnGround", isTouchingGround);
        playerAnimator.SetBool("isAttacking", isAttacking);
    }

    void RotatePlayer(bool toRight)
    {
        if (toRight != isLookingRight)
        {
            isLookingRight = !isLookingRight;
            transform.Rotate(0,180,0);
        }
    }

    void MoveHandler()
    {
        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            RotatePlayer(true);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            RotatePlayer(false);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }
    }

    void KnockbackHandler()
    {
        if (knockFromRight)
        {
            player.velocity = new Vector2(-kbForce, 0.2f * kbForce);
        }
        else
        {
            player.velocity = new Vector2(kbForce, 0.2f * kbForce);
        }

        kbCounter -= Time.deltaTime;
    }
    

    void JumpHandler()
    {
        if (isTouchingGround)
        {
            doubleJump = true;
        }
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            doubleJump = true;
        }

        else if (Input.GetButtonDown("Jump") && doubleJump)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            doubleJump = false;
        }
    }

    void AttackHandler()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
            attackArea.SetActive(isAttacking);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            isAttacking = false;
            attackArea.SetActive(isAttacking);
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0;
                isAttacking = false;
                attackArea.SetActive(isAttacking);
            }
        }
        else
        {
            attackArea.SetActive(isAttacking);
            attackTimer = 0.0f;
        }
    }

    public void takeKnockback(float force, bool fromRight, float time = 0.25f)
    {
        knockFromRight = fromRight;
        kbForce = force;
        kbCounter = time;
    }
}
