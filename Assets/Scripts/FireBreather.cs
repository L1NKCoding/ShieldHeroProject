using UnityEngine;

public class FireBreather : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject fireballPrefab;      // assign in Inspector
    public Transform firePoint;           // where the fireball comes from
    public float fireballSpeed = 8f;
    public float fireRate = 1.5f;         // seconds between shots
    public float detectRange = 12f;       // how far it can see the player

    Transform player;
    float nextFireTime;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Only shoot if player is in range horizontally
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Decide direction toward player (left or right)
        int dir = player.position.x >= transform.position.x ? 1 : -1;

        // Spawn fireball
        Transform spawnPoint = firePoint != null ? firePoint : transform;
        GameObject fb = Instantiate(fireballPrefab, spawnPoint.position, Quaternion.identity);

        // Give it velocity
        Rigidbody2D rb = fb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(dir * fireballSpeed, 0f);
        }
    }

    // Call this when a reflected fireball hits the enemy
    public void Die()
    {
        // TODO later trigger FireBreatherDead animation
        Destroy(gameObject);
    }
}
