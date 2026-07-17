using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;

    public void SelectLevel()
    {
        GameData.SelectedLevel = levelIndex;

        SceneManager.LoadScene("CarSelection");
    }
}