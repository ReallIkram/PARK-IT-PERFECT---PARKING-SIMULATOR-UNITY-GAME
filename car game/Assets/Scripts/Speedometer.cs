using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public Rigidbody carRb;

    private void Update()
    {
        float speed = carRb.linearVelocity.magnitude * 3.6f;

        UIManager.Instance.UpdateSpeed(speed);
    }
}