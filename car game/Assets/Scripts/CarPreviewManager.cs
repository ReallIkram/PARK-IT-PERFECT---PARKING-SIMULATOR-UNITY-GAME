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

        ShowCar();
    }

    void ShowCar()
    {
        if (currentCar != null)
            Destroy(currentCar);

        currentCar = Instantiate(
            previewCars[currentCarIndex],
            spawnPoint.position,
            spawnPoint.rotation
        );

        // Rotate preview
        if (currentCar.GetComponent<RotatePreview>() == null)
            currentCar.AddComponent<RotatePreview>();

        // Save current preview index
        PlayerPrefs.SetInt("PreviewCar", currentCarIndex);
    }

    // Next Button
    public void NextCar()
    {
        currentCarIndex++;

        if (currentCarIndex >= previewCars.Length)
            currentCarIndex = 0;

        ShowCar();
    }

    // Previous Button
    public void PreviousCar()
    {
        currentCarIndex--;

        if (currentCarIndex < 0)
            currentCarIndex = previewCars.Length - 1;

        ShowCar();
    }

    // Play Button
    public void SelectCar()
    {
        // Save selected car for gameplay
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
        PlayerPrefs.Save();

        SceneManager.LoadScene("LevelSelection");
    }

    // Back Button
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }
}