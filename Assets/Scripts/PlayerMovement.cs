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

    /// <summary>
    /// Components
    /// </summary>
    private Rigidbody2D body;
    private BoxCollider2D selfCollider;
    private Animator animator;

    private bool IsFacingLeft = false;
    private float HorizontalInput;
    private bool CanJump;
    private bool Grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        selfCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(HorizontalInput * Speed, body.velocity.y);

        MonitorFlip();

        if (Input.GetKeyDown(KeyCode.Space) && CanJump)
        { 
            Jump(); 
        }

        animator.SetBool("run", HorizontalInput != 0);
        animator.SetBool("grounded", Grounded);

    }

    private void MonitorFlip()
    {
        if ((HorizontalInput < -0.01f && !IsFacingLeft) || (HorizontalInput > 0.01f && IsFacingLeft))
        {
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            IsFacingLeft = !IsFacingLeft;
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, Speed);
        Grounded = false;
        animator.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Grounded = true;
            CanJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            CanJump = false;
        }
    }
}
