using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    [SerializeField] private TextMeshProUGUI timerText;

    private bool timerStarted = false;
    private float timeElapsed = 0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (timerText == null)
            Debug.LogError("GameTimer: Timer Text is NOT assigned!");
    }

    private void Update()
    {
        if (!timerStarted && Input.anyKeyDown)
        {
            timerStarted = true;
        }

        if (!timerStarted)
            return;

        timeElapsed += Time.deltaTime;

        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed % 60f);

            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public float GetElapsedTime()
    {
        return timeElapsed;
    }

    public void ResetTimer()
    {
        timerStarted = false;
        timeElapsed = 0f;

        if (timerText != null)
            timerText.text = "00:00";
    }
}