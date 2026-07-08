using UnityEngine;

public class NightLightingManager : MonoBehaviour
{
    [Header("Lighting Settings")]
    [SerializeField] private Light mainDirectionalLight; // Your sun/main light
    [SerializeField] private float nightAmbientIntensity = 0.3f;
    [SerializeField] private Color nightAmbientColor = new Color(0.1f, 0.15f, 0.25f); // Dark blue
    
    [Header("Scene Atmosphere")]
    [SerializeField] private bool usePostProcessing = true;
    [SerializeField] private Color fogColor = new Color(0.05f, 0.08f, 0.15f); // Night fog color
    [SerializeField] private float fogDensity = 0.02f;
    
    void Start()
    {
        SetupNightMode();
    }
    
    void SetupNightMode()
    {
        // Dim the main sun light
        if (mainDirectionalLight != null)
        {
            mainDirectionalLight.intensity = 0.1f; // Very dim moonlight
            mainDirectionalLight.color = new Color(0.6f, 0.7f, 1f); // Cool blue light
        }
        
        // Set ambient lighting for night
        RenderSettings.ambientLight = nightAmbientColor;
        RenderSettings.ambientIntensity = nightAmbientIntensity;
        
        // Setup fog for night atmosphere
        RenderSettings.fog = true;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = fogDensity;
        
        // Adjust skybox if you have one
        if (RenderSettings.skybox != null)
        {
            // Make skybox darker
            RenderSettings.skybox.SetFloat("_Exposure", 0.3f);
        }
    }
}