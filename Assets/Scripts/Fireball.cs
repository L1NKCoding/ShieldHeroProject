using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float lifeTime = 5f;

    bool reflected = false;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If it hits shield, reflect
        if (other.CompareTag("Shield") && !reflected)
        {
            reflected = true;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(-rb.linearVelocity.x, rb.linearVelocity.y);
            }
            gameObject.layer = LayerMask.NameToLayer("Player Projectile"); 
        }
        // If a reflected fireball hits FireBreather, kill it
        else if (other.CompareTag("Enemy") && reflected)
        {
            FireBreather fb = other.GetComponent<FireBreather>();
            if (fb != null)
            {
                fb.Die();
            }
            Destroy(gameObject);
        }
        // If it hits the player (not through shield), kill them
        else if (other.CompareTag("Player"))
        {
            KillPlayer(other.gameObject);
            Destroy(gameObject);
        }
        // If it hits a wall, destroy the fireball
        else if (other.CompareTag("Wall") || other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle physical collisions with walls/ground
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
    
    void KillPlayer(GameObject player)
    {
        Destroy(player);
        Debug.Log("Player killed by fireball!");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Death_Screen");
    }
}