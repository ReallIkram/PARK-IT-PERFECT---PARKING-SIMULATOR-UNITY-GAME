using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void SelectLevel(string levelName)
    {
        GameManager.Instance.selectedLevel = levelName;

        SceneManager.LoadScene("CarSelection");
    }
}