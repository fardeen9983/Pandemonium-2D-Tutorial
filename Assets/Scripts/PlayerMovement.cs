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
    private GameObject Ground;

    /// <summary>
    /// Components
    /// </summary>
    private Rigidbody2D body;
    private BoxCollider2D selfCollider;
    private SpriteRenderer spriteRenderer;

    private bool IsFacingLeft = false;
    private float HorizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        selfCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(HorizontalInput * Speed, body.velocity.y);

        MonitorFlip();

        if (Input.GetKeyDown(KeyCode.Space) && selfCollider.IsTouching(Ground.GetComponent<BoxCollider2D>()))
        {
            body.velocity = new Vector2(body.velocity.x, Speed);
        }
    }

    void MonitorFlip()
    {
        
        if ((HorizontalInput < -0.01f && !IsFacingLeft) || (HorizontalInput > 0.01f && IsFacingLeft))
        {
            Vector2 scale = transform.localScale;
            scale.x *=  -1;
            transform.localScale = scale;
            IsFacingLeft = !IsFacingLeft;
        }
    }
}
