using UnityEngine;

public class CarHeadlights : MonoBehaviour
{
    [Header("Headlight Settings")]
    [SerializeField] private Light[] headlights; // Assign both headlights in inspector
    [SerializeField] private float headlightIntensity = 1.5f;
    [SerializeField] private float headlightRange = 50f;
    [SerializeField] private float spotAngle = 40f;
    [SerializeField] private Color headlightColor = new Color(1f, 0.95f, 0.8f); // Warm white
    
    [Header("Brake Lights")]
    [SerializeField] private Light[] brakeLights; // Optional: assign brake lights
    [SerializeField] private float brakeIntensity = 1f;
    [SerializeField] private Color brakeColor = new Color(1f, 0.1f, 0.1f); // Red
    
    private Rigidbody carRigidbody;
    private float currentSpeed = 0f;
    private float brakingThreshold = 0.1f; // Speed threshold for brake lights
    
    void Start()
    {
        // Setup headlights
        if (headlights.Length == 0)
        {
            Debug.LogWarning("No headlights assigned to CarHeadlights script!");
            return;
        }
        
        carRigidbody = GetComponent<Rigidbody>();
        
        SetupHeadlights();
        SetupBrakeLights();
    }
    
    void SetupHeadlights()
    {
        foreach (Light light in headlights)
        {
            if (light != null)
            {
                light.type = LightType.Spot;
                light.intensity = headlightIntensity;
                light.range = headlightRange;
                light.spotAngle = spotAngle;
                light.color = headlightColor;
                light.enabled = true;
            }
        }
    }
    
    void SetupBrakeLights()
    {
        foreach (Light light in brakeLights)
        {
            if (light != null)
            {
                light.type = LightType.Point;
                light.intensity = brakeIntensity;
                light.range = 20f;
                light.color = brakeColor;
                light.enabled = false; // Off by default
            }
        }
    }
    
    void Update()
    {
        if (carRigidbody != null)
        {
            currentSpeed = carRigidbody.linearVelocity.magnitude;
            
            // Show brake lights when slowing down or in reverse
            bool isBraking = currentSpeed < brakingThreshold;
            
            foreach (Light light in brakeLights)
            {
                if (light != null)
                {
                    light.enabled = isBraking;
                }
            }
        }
    }
}