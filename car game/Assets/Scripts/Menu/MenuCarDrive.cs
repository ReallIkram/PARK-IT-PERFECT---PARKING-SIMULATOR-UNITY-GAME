using UnityEngine;

public class MenuCarDrive : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}