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

        // Spawn selected car
        spawnedCar = Instantiate(
            cars[selectedCar],
            spawnPoint.position,
            spawnPoint.rotation
        );

        // Rotate if required
        spawnedCar.transform.Rotate(0f, 270f, 0f);

        // Get Rigidbody
        rb = spawnedCar.GetComponent<Rigidbody>();

        // Assign all Cinemachine cameras
        CinemachineCamera[] cameras =
            FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);

        foreach (var cam in cameras)
        {
            cam.Follow = spawnedCar.transform;
            cam.LookAt = spawnedCar.transform;
        }

        // -----------------------------
        // ENGINE SOUND SETUP
        // -----------------------------
        AudioSource engine = spawnedCar.GetComponentInChildren<AudioSource>(true);

        if (engine != null)
        {
            engine.playOnAwake = false;
            engine.loop = true;
            engine.Stop();

            CarEngineSound sound = spawnedCar.AddComponent<CarEngineSound>();
            sound.engineSound = engine;
        }
        else
        {
            Debug.LogWarning("No AudioSource found inside the spawned car.");
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