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
    public GameObject shieldObject;        // Assign your shield GameObject in Inspector
    public Sprite normalSprite;            // Assign normal character sprite in Inspector
    public Sprite shieldSprite;            // Assign shield-up character sprite in Inspector

    private Rigidbody2D rb;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private bool isShieldActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = playerBody.GetComponent<SpriteRenderer>();
        
        // Initialize shield as inactive
        if (shieldObject != null)
        {
            shieldObject.SetActive(false);
        }
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

        // Shield based on movement - active when NOT moving
        if (moveInputX == 0)
        {
            ActivateShield();
        }
        else
        {
            DeactivateShield();
        }
    }

    void ActivateShield()
    {
        if (!isShieldActive)
        {
            isShieldActive = true;
            
            // Enable shield GameObject
            if (shieldObject != null)
            {
                shieldObject.SetActive(true);
            }
            
            // Change sprite to shield version
            if (spriteRenderer != null && shieldSprite != null)
            {
                spriteRenderer.sprite = shieldSprite;
            }
        }
    }

    void DeactivateShield()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            
            // Disable shield GameObject
            if (shieldObject != null)
            {
                shieldObject.SetActive(false);
            }
            
            // Change sprite back to normal
            if (spriteRenderer != null && normalSprite != null)
            {
                spriteRenderer.sprite = normalSprite;
            }
        }
    }
}
