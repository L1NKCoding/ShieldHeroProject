using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour

{
   void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("You Win!");
            // Store the current level index before loading win screen
            PlayerPrefs.SetInt("CompletedLevel", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("Win_Screen");
        }
    }
}