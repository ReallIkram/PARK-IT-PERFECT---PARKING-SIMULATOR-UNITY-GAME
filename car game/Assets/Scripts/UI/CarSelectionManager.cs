using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CarSelectionManager : MonoBehaviour
{
    [Header("All Levels")]
    public LevelCars[] levels;

    [Header("UI")]
    public TMP_Text levelTitle;
    public TMP_Text carName;
    public Image carImage;
    public Image mapPreview;

    private int currentCar = 0;
    private LevelCars selectedLevel;

    void Start()
    {
        string level = PlayerPrefs.GetString("SelectedLevel");

        foreach (LevelCars l in levels)
        {
            if (l.levelName == level)
            {
                selectedLevel = l;
                break;
            }
        }

        if (selectedLevel == null)
        {
            Debug.LogError("Level not found!");
            return;
        }

        levelTitle.text = selectedLevel.levelName;
        mapPreview.sprite = selectedLevel.mapPreview;

        ShowCar();
    }

    void ShowCar()
    {
        carImage.sprite = selectedLevel.cars[currentCar].carSprite;
        carName.text = selectedLevel.cars[currentCar].carName;
    }

    public void NextCar()
    {
        currentCar++;

        if (currentCar >= selectedLevel.cars.Length)
            currentCar = 0;

        ShowCar();
    }

    public void PreviousCar()
    {
        currentCar--;

        if (currentCar < 0)
            currentCar = selectedLevel.cars.Length - 1;

        ShowCar();
    }

    public void Play()
    {
        PlayerPrefs.SetInt("SelectedCar", currentCar);

        SceneManager.LoadScene(selectedLevel.sceneName);
    }
}