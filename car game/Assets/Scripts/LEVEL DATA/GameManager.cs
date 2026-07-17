using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string selectedLevel;
    public int selectedCar;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}