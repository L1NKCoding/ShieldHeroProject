using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    
    private Rigidbody2D rb;
    private int direction = -1; // -1 for left, 1 for right
    
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
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we hit a wall or any solid object
        // You can add specific tags like "Wall" if needed: collision.gameObject.CompareTag("Wall")
        if (collision.contacts.Length > 0)
        {
            // Check if collision is from the side (horizontal collision)
            Vector2 normal = collision.contacts[0].normal;
            if (collision.gameObject.CompareTag("Wall") && Mathf.Abs(normal.x) > 0.5f)
            {
                // Turn around
                direction *= -1;
                flipSprite();
            }
        }
    }

    void flipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
