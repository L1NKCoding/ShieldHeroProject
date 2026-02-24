using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}