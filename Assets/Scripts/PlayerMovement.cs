using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Script Input
    /// </summary>
    [SerializeField]
    private float Speed = 10;
    [SerializeField]
    private float JumpPower;
    [SerializeField]
    private LayerMask GroundLayer;
    [SerializeField]
    private LayerMask WallLayer;

    /// <summary>
    /// Components
    /// </summary>
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private bool IsFacingLeft = false;
    private float HorizontalInput;
    private float VerticalInput;
    private float WallJumpCooldown;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");

        if ((HorizontalInput < -0.01f && !IsFacingLeft) || (HorizontalInput > 0.01f && IsFacingLeft))
        {
            Flip();
        }

        animator.SetBool("run", HorizontalInput != 0);
        animator.SetBool("grounded", IsGrounded());

        if (WallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(HorizontalInput * Speed, body.velocity.y);

            if (OnWall() && !IsGrounded())
            {
                body.gravityScale = 0;
                body.velocity = new Vector2(0, 0);
            }
            else
            {
                body.gravityScale = 2;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            WallJumpCooldown += Time.deltaTime;
        }
    }

    private void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        IsFacingLeft = !IsFacingLeft;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, JumpPower);
            animator.SetTrigger("jump");
        }
        else if (OnWall() && !IsGrounded())
        {
            WallJumpCooldown = 0;
            if (HorizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, -1);
                Flip();
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }

        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);
        return raycastHit.collider != null;
    }
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, WallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return HorizontalInput == 0 && IsGrounded() && !OnWall();
    }


}
