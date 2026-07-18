using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreenController : MonoBehaviour
{
    public Slider loadingSlider;
    public TMP_Text loadingPercentText;

    private string sceneToLoad;

    void Start()
    {
        // Get the level selected in the Level Selection scene
        sceneToLoad = PlayerPrefs.GetString("SelectedLevel", "Level1");

        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        float fakeProgress = 0f;

        while (!operation.isDone)
        {
            float realProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Smooth loading bar animation
            fakeProgress = Mathf.MoveTowards(fakeProgress, realProgress, Time.deltaTime * 0.5f);

            if (loadingSlider != null)
                loadingSlider.value = fakeProgress;

            if (loadingPercentText != null)
                loadingPercentText.text = Mathf.RoundToInt(fakeProgress * 100f) + "%";

            // When loading reaches 100%
            if (operation.progress >= 0.9f && fakeProgress >= 1f)
            {
                if (loadingSlider != null)
                    loadingSlider.value = 1f;

                if (loadingPercentText != null)
                    loadingPercentText.text = "100%";

                // Keep loading screen visible for 3 seconds
                yield return new WaitForSeconds(3f);

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}