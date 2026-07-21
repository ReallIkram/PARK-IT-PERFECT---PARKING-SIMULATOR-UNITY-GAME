using UnityEngine;
using Unity.Cinemachine;

public class CarSpawner : MonoBehaviour
{
    [Header("Spawn")]
    public Transform spawnPoint;
    public GameObject[] cars;

    private GameObject spawnedCar;
    private Rigidbody rb;

    void Start()
    {
        int selectedCar = PlayerPrefs.GetInt("SelectedCar", 0);

        // Prevent invalid index
        if (selectedCar < 0 || selectedCar >= cars.Length)
            selectedCar = 0;

        // Spawn selected car
        spawnedCar = Instantiate(
            cars[selectedCar],
            spawnPoint.position,
            spawnPoint.rotation
        );

        // Rotate if required
        spawnedCar.transform.Rotate(0f, 270f, 0f);
        // Assign minimap target
MinimapFollow minimap = FindFirstObjectByType<MinimapFollow>();

if (minimap != null)
{
    minimap.SetTarget(spawnedCar.transform);
}
        // Cache Rigidbody
        rb = spawnedCar.GetComponent<Rigidbody>();

        // Assign all Cinemachine cameras
        CinemachineCamera[] cameras =
            FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);

        foreach (CinemachineCamera cam in cameras)
        {
            cam.Follow = spawnedCar.transform;
            cam.LookAt = spawnedCar.transform;
        }

        // Engine sound setup
        AudioSource engine = spawnedCar.GetComponentInChildren<AudioSource>(true);

        if (engine == null)
        {
            Debug.LogWarning("No AudioSource found inside the spawned car.");
        }
        else
        {
            engine.playOnAwake = false;
            engine.loop = true;
            engine.Stop();

            // Add CarEngineSound only if missing
            CarEngineSound sound = spawnedCar.GetComponent<CarEngineSound>();

            if (sound == null)
                sound = spawnedCar.AddComponent<CarEngineSound>();

            // DO NOT assign engineSound here.
            // CarEngineSound will automatically find the AudioSource.
        }
    }

    void Update()
    {
        if (rb == null)
            return;

        float speed = rb.linearVelocity.magnitude * 3.6f;

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateSpeed(speed);
    }
}