using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void RestartGame()
    {
        SetCurrentLevel(1);
        SceneManager.LoadScene("Level_1");
    }
    
    public void ReplayLevel()
    {
        int currentLevel = GetCurrentLevel();
        if (currentLevel > 0)
        {
            SceneManager.LoadScene("Level_" + currentLevel);
        }
        else
        {
            SceneManager.LoadScene("Level_1");
        }
    }
    
    public void NextLevel()
    {
        int currentLevel = GetCurrentLevel();
        int nextLevel = currentLevel + 1;
        SetCurrentLevel(nextLevel);
        SceneManager.LoadScene("Level_" + nextLevel);
    }
    
    private int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel", 1);
    }
    
    private void SetCurrentLevel(int level)
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}