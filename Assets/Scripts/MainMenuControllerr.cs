using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    void Start()
    {
        // Load saved settings on app start
        GameSettings.Load();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        Application.Quit(); // Works in build, not in editor
    }
}