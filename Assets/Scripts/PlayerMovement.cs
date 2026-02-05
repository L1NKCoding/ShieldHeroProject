using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float jumpForce = 20f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    [SerializeField]
    public GameObject playerBody;

    private Rigidbody2D rb;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        Vector2 move = new Vector2(moveInputX * speed, rb.linearVelocity.y);
        rb.linearVelocity = move;

        // Flip sprite
        if (moveInputX < 0)
            playerBody.transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (moveInputX > 0)
            playerBody.transform.rotation = Quaternion.Euler(0, 0, 0);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
