using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;          // ADD THIS

    public TextMeshProUGUI timerText;

    private bool timerStarted = false;
    private float timeElapsed = 0f;

    void Awake()
    {
        Instance = this;                       // ADD THIS
    }

    void Update()
    {
        if (!timerStarted && Input.anyKeyDown)
        {
            timerStarted = true;
        }

        if (timerStarted)
        {
            timeElapsed += Time.deltaTime;

            float minutes = Mathf.FloorToInt(timeElapsed / 60);
            float seconds = Mathf.FloorToInt(timeElapsed % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public float GetElapsedTime()              // ADD THIS
    {
        return timeElapsed;
    }
}