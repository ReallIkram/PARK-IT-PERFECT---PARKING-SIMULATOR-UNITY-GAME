using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreenController : MonoBehaviour
{
    public Slider loadingSlider;
    public TMP_Text loadingPercentText;
    public string sceneToLoad = "Level1";

    void Start()
    {
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

            // slowly move fake progress toward real progress
            fakeProgress = Mathf.MoveTowards(fakeProgress, realProgress, Time.deltaTime * 0.3f);

            if (loadingSlider != null)
                loadingSlider.value = fakeProgress;

            if (loadingPercentText != null)
                loadingPercentText.text = Mathf.RoundToInt(fakeProgress * 100f) + "%";

            // when fully loaded wait 3 seconds then go to Level1
            if (operation.progress >= 0.9f && fakeProgress >= 0.99f)
            {
                loadingSlider.value = 1f;
                loadingPercentText.text = "100%";
                yield return new WaitForSeconds(3f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}