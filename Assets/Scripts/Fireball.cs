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
            gameObject.layer = LayerMask.NameToLayer("PlayerProjectile"); 
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
        // If it hits ground or player, just destroy 
        else if (other.CompareTag("Ground") || other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
