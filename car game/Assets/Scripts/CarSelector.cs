using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelector : MonoBehaviour
{
    public void OpenPreview(int carIndex)
    {
        // Save which car should be previewed
        PlayerPrefs.SetInt("PreviewCar", carIndex);

        // Also save it as the currently selected car
        PlayerPrefs.SetInt("SelectedCar", carIndex);

        // Open the preview scene
        SceneManager.LoadScene("CarPreview");
    }
}