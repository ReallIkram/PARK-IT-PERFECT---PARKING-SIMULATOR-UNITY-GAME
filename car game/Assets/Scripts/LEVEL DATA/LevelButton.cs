using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    // Exact scene name of this level
    public string levelSceneName;

    public void SelectLevel()
    {
        // Save the selected level
        PlayerPrefs.SetString("SelectedLevel", levelSceneName);

        // Save to disk
        PlayerPrefs.Save();

        // Load the loading scene (or load the level directly)
        SceneManager.LoadScene("LoadingScreen");
        // OR
        // SceneManager.LoadScene(levelSceneName);
    }
}