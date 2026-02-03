using UnityEngine;

public class Died : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            {
            Destroy(other.gameObject);
            Debug.Log("You Lose");
        }
    }
}
