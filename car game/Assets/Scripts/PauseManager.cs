using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject pausePanel;

    [Header("Settings")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] private bool hideCursorWhenPlaying = true;

    public bool IsPaused => pausePanel != null && pausePanel.activeSelf;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        ResumeGame();
    }

    private void Update()
    {
        // Don't allow pause if Game Over or Success panel is open
        if (GameOverManager.Instance != null)
        {
            if ((GameOverManager.Instance.gameOverPanel != null &&
                 GameOverManager.Instance.gameOverPanel.activeSelf) ||
                (GameOverManager.Instance.successPanel != null &&
                 GameOverManager.Instance.successPanel.activeSelf))
            {
                return;
            }
        }

        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    // Toggle with ESC
    public void TogglePause()
    {
        if (IsPaused)
            ResumeGame();
        else
            PauseGame();
    }

    // Called from ESC or Pause Button
    public void PauseGame()
    {
        if (pausePanel == null)
        {
            Debug.LogError("Pause Panel is not assigned!");
            return;
        }

        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Called from Continue Button
    public void ResumeGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;

        if (hideCursorWhenPlaying)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Button Function
    public void ContinueButton()
    {
        ResumeGame();
    }

    // Button Function
    public void RestartButton()
    {
        ResumeGame();

        if (GameOverManager.Instance != null)
            GameOverManager.Instance.Restart();
    }

    // Button Function
    public void MainMenuButton()
    {
        ResumeGame();

        if (GameOverManager.Instance != null)
            GameOverManager.Instance.GoToMainMenu();
    }

    // Button Function
    public void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}