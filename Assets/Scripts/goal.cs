using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour

{
   void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
             Debug.Log("You Win!");
            //SceneManager.LoadScene(winscreen);
           

    }
}
