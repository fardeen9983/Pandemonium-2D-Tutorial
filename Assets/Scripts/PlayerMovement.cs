using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10;

    [SerializeField]
    private GameObject Ground;

    private Rigidbody2D body;
    private BoxCollider2D selfCollider;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        
        selfCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed, body.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && selfCollider.IsTouching(Ground.GetComponent<BoxCollider2D>()))
        {
            body.velocity = new Vector2(body.velocity.x,Speed);
        }
    }
}
