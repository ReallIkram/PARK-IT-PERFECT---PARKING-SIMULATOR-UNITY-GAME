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
        PrometeoCarController controller =
            spawnedCar.GetComponent<PrometeoCarController>();

        if (controller != null)
        {
            AudioSource engine =
                spawnedCar.GetComponentInChildren<AudioSource>(true);

            if (engine != null)
            {
                controller.useSounds = true;
                controller.carEngineSound = engine;

                // Make sure the engine loops
                engine.loop = true;

                // Start with idle pitch
                engine.pitch = 0.8f;

                // Play engine
                if (!engine.isPlaying)
                    engine.Play();
            }
            else
            {
                Debug.LogWarning("No Engine AudioSource found inside the spawned car.");
            }
        }

        // -----------------------------
        // OPTIONAL: Tire Screech Sound
        // -----------------------------
        /*
        AudioSource[] sounds = spawnedCar.GetComponentsInChildren<AudioSource>(true);

        if (sounds.Length > 1)
        {
            controller.tireScreechSound = sounds[1];
        }
        */
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