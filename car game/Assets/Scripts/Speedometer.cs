using UnityEngine;

public class Speedometer : MonoBehaviour
{
    private Rigidbody carRb;

    void Start()
    {
        FindCar();
    }

    void FindCar()
    {
        GameObject playerCar = GameObject.FindGameObjectWithTag("Player");

        if (playerCar != null)
        {
            carRb = playerCar.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        // If the car hasn't spawned yet, keep looking
        if (carRb == null)
        {
            FindCar();
            return;
        }

        float speed = carRb.linearVelocity.magnitude * 3.6f;
        UIManager.Instance.UpdateSpeed(speed);
    }
}