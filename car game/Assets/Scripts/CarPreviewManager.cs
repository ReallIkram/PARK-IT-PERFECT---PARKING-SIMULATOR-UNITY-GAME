using UnityEngine;
using UnityEngine.SceneManagement;

public class CarPreviewManager : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] previewCars;

    private GameObject currentCar;
    private int currentCarIndex;

    void Start()
    {
        currentCarIndex = PlayerPrefs.GetInt("PreviewCar", 0);

        currentCar = Instantiate(
            previewCars[currentCarIndex],
            spawnPoint.position,
            spawnPoint.rotation
        );

        // Make the preview car rotate
        currentCar.AddComponent<RotatePreview>();
    }

    public void SelectCar()
    {
        // Save the selected car for gameplay
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);

        // Go to Level Select
        SceneManager.LoadScene("LevelSelect");
    }

    public void Back()
    {
        SceneManager.LoadScene("CarSelect");
    }
}