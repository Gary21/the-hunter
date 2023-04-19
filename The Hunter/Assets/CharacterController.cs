using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 5f;
    private float direction = 0f;
    private Rigidbody2D player;
    private bool isLookingRight = true;
    private bool isMoving = false;
    private bool isAttacking = false;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        MoveHandler();
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

    void JumpHandler()
    {
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }
        
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }
    }

    void AttackHandler()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            isAttacking = false;
        }
    }
}
