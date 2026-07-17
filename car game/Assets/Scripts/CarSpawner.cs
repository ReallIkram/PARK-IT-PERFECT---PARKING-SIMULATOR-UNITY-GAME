using UnityEngine;
using Unity.Cinemachine;

public class CarSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] cars;

    void Start()
    {
        int selectedCar = PlayerPrefs.GetInt("SelectedCar", 0);

       GameObject spawnedCar = Instantiate(
    cars[selectedCar],
    spawnPoint.position,
    spawnPoint.rotation
);

// Rotate the car after spawning
spawnedCar.transform.Rotate(0f, 270f, 0f);

        CinemachineCamera[] cameras = FindObjectsByType<CinemachineCamera>(
            FindObjectsSortMode.None
        );

        foreach (var cam in cameras)
        {
            cam.Follow = spawnedCar.transform;
            cam.LookAt = spawnedCar.transform;
        }
    }
}