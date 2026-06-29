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

        Time.timeScale = 1f;

        if (pausePanel != null)
            pausePanel.SetActive(false);
        else
            Debug.LogError("Pause Panel is NOT assigned!");
    }

    private void Update()
    {
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

    public void TogglePause()
    {
        if (IsPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        Debug.Log("PauseGame()");

        if (pausePanel == null)
            return;

        pausePanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Debug.Log("ResumeGame()");

        if (pausePanel == null)
            return;

        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        if (hideCursorWhenPlaying)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void RestartGame()
    {
        ResumeGame();

        if (GameOverManager.Instance != null)
            GameOverManager.Instance.Restart();
    }

    public void LoadMainMenu()
    {
        ResumeGame();

        if (GameOverManager.Instance != null)
            GameOverManager.Instance.GoToMainMenu();
    }

    public void QuitGame()
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