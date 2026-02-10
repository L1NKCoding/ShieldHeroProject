using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    
    [Header("Detection")]
    public float wallCheckDistance = 0.5f;
    public float ledgeCheckDistance = 1f;
    public float ledgeCheckOffsetX = 0.6f; // How far ahead to check for ledges
    public float ledgeCheckOffsetY = -0.6f; // How far down from center to start checking
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private int direction = -1; // -1 for left, 1 for right
    private bool facingLeft = true;
    private float lastFlipTime = 0f;
    private float flipCooldown = 0.2f; // Cooldown between flips in seconds
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Constantly move the enemy
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        }
        
        // Calculate check positions
        Vector2 wallCheckPos = new Vector2(transform.position.x + (wallCheckDistance * direction), transform.position.y);
        Vector2 ledgeCheckPos = new Vector2(transform.position.x + (wallCheckDistance * direction), transform.position.y + ledgeCheckOffsetY);
        
        // Check for walls ahead
        bool hitWall = Physics2D.Raycast(wallCheckPos, Vector2.right * direction, wallCheckDistance, groundLayer);
        
        // Check for ledges (no ground ahead)
        bool hasGroundAhead = Physics2D.Raycast(ledgeCheckPos, Vector2.down, ledgeCheckDistance, groundLayer);
        
        // Turn around if we hit a wall or detect a ledge (with cooldown)
        if ((hitWall || !hasGroundAhead) && Time.time - lastFlipTime > flipCooldown)
        {
            Flip();
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player touched the enemy
        if (collision.gameObject.CompareTag("Player"))
        {
            KillPlayer(collision.gameObject);
            return;
        }
        
        // Backup collision detection for walls
        if (collision.contacts.Length > 0)
        {
            // Check if collision is from the side (horizontal collision)
            Vector2 normal = collision.contacts[0].normal;
            if (Mathf.Abs(normal.x) > 0.5f)
            {
                Flip();
            }
        }
    }
    
    void KillPlayer(GameObject player)
    {
        Destroy(player);
        Debug.Log("Player killed by enemy!");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Death_Screen");
    }

    void Flip()
    {
        // Record flip time
        lastFlipTime = Time.time;
        
        // Change direction
        direction *= -1;
        facingLeft = !facingLeft;
        
        // Flip the sprite
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    // Visualize detection rays in editor
    void OnDrawGizmos()
    {
        // Calculate check positions
        Vector2 wallCheckPos = new Vector2(transform.position.x + (wallCheckDistance * direction), transform.position.y);
        Vector2 ledgeCheckPos = new Vector2(transform.position.x + (wallCheckDistance * direction), transform.position.y + ledgeCheckOffsetY);
        
        // Draw wall check ray
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheckPos, wallCheckPos + Vector2.right * direction * wallCheckDistance);
        Gizmos.DrawWireSphere(wallCheckPos, 0.1f);
        
        // Draw ledge check ray
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(ledgeCheckPos, ledgeCheckPos + Vector2.down * ledgeCheckDistance);
        Gizmos.DrawWireSphere(ledgeCheckPos, 0.1f);
    }
}
