using UnityEngine;

public class StreetLightController : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private Light streetLight;
    [SerializeField] private float nightIntensity = 1.2f;
    [SerializeField] private float lightRange = 30f;
    [SerializeField] private Color streetLightColor = new Color(1f, 0.9f, 0.7f); // Warm yellow
    
    [Header("Visual Enhancement (Optional)")]
    [SerializeField] private bool addGlow = true;
    [SerializeField] private Material glowMaterial; // Optional: mat with glow/emission
    
    void Start()
    {
        SetupStreetLight();
    }
    
    void SetupStreetLight()
    {
        if (streetLight == null)
        {
            streetLight = GetComponent<Light>();
        }
        
        if (streetLight != null)
        {
            streetLight.type = LightType.Point;
            streetLight.intensity = nightIntensity;
            streetLight.range = lightRange;
            streetLight.color = streetLightColor;
            streetLight.enabled = true;
            
            // Add slight shadow for realism
            streetLight.shadows = LightShadows.Soft;
            streetLight.shadowBias = 0.01f;
        }
        else
        {
            Debug.LogWarning("StreetLightController: No Light component found on " + gameObject.name);
        }
        
        // Apply glow material if assigned
        if (addGlow && glowMaterial != null)
        {
            GetComponent<Renderer>().material = glowMaterial;
        }
    }
}