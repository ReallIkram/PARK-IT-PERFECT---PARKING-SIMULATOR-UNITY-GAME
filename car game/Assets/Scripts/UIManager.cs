using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Top Left")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI missionText;

    [Header("Bottom Left")]
    public TextMeshProUGUI speedText;

    private float elapsedTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        missionText.text = "Park in the Parking Area";
        speedText.text = "0";
        timerText.text = "00:00";
    }

    private void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);

        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public void SetMission(string mission)
    {
        missionText.text = mission;
    }

    public void UpdateSpeed(float speed)
    {
        speedText.text = Mathf.RoundToInt(speed).ToString();
    }
}