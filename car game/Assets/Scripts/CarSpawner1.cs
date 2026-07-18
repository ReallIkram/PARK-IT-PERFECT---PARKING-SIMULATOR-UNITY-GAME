using UnityEngine;

public class CarSpawnerRender : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] carPrefabs;

    void Start()
    {
        int selectedCar = PlayerPrefs.GetInt("SelectedCar", 0);

        GameObject car = Instantiate(
            carPrefabs[selectedCar],
            spawnPoint.position,
            spawnPoint.rotation
        );

        MinimapFollow minimap = FindObjectOfType<MinimapFollow>();

        if (minimap != null)
        {
            minimap.target = car.transform;
        }
    }
}