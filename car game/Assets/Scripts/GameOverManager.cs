using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("Panels")]
    public GameObject gameOverPanel;
    public GameObject successPanel;

    [Header("Star Images")]
    public Image star1;
    public Image star2;
    public Image star3;

    [Header("Star Sprites")]
    public Sprite starFilled;
    public Sprite starEmpty;

    [Header("Time Thresholds (seconds)")]
    public float threeStarTime = 20f;
    public float twoStarTime   = 40f;
    // anything above twoStarTime = 1 star

    void Awake()
    {
        Instance = this;
    }

    // Called when the player crashes
    public void ShowCrash()
    {
        Time.timeScale = 0f;          // Pause the game
        gameOverPanel.SetActive(true);
    }

    // Called when the player parks successfully
    public void ShowSuccess(float elapsedTime)
    {
        Time.timeScale = 0f;
        successPanel.SetActive(true);

        int stars = GetStars(elapsedTime);
        star1.sprite = stars >= 1 ? starFilled : starEmpty;
        star2.sprite = stars >= 2 ? starFilled : starEmpty;
        star3.sprite = stars >= 3 ? starFilled : starEmpty;
    }

    int GetStars(float time)
    {
        if (time <= threeStarTime) return 3;
        if (time <= twoStarTime)  return 2;
        return 1;
    }

    // Assign these to your Button OnClick() events
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu"); // Use your actual scene name
    }
}