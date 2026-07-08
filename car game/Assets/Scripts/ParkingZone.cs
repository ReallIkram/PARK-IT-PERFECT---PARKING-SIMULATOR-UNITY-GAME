using UnityEngine;

public class ParkingZone : MonoBehaviour
{
    private Renderer rend;
    private Rigidbody carRb;
    private Transform car;

    public Color idleColor = new Color(0f, 1f, 0f, 0.3f);
    public Color partialColor = new Color(1f, 0f, 0f, 0.5f);
    public Color perfectColor = new Color(0f, 1f, 0f, 0.7f);

    private Bounds zoneBounds;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = idleColor;

        zoneBounds = GetComponent<Collider>().bounds;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Inside Trigger: " + other.name);

        Debug.Log("Trigger Stay: " + other.name + " | Root: " + other.transform.root.name + " | Tag: " + other.transform.root.tag); 

        Transform root = other.transform.root;

        if (!root.CompareTag("Player")) return;

        car = root;
        carRb = root.GetComponent<Rigidbody>();

        zoneBounds = GetComponent<Collider>().bounds;
        Bounds carBounds = GetCarBounds(root);

        bool fullyInside = zoneBounds.Contains(carBounds.min) &&
                           zoneBounds.Contains(carBounds.max);

        float speed = carRb != null ? carRb.linearVelocity.magnitude : 999f;

        // 🔴 PARTIAL INSIDE
        if (!fullyInside)
        {
            rend.material.color = partialColor;
            return;
        }

        // 🟢 FULLY INSIDE BUT MOVING
        if (speed > 1f)
        {
            rend.material.color = partialColor;
            return;
        }

        // 🟢 PERFECT PARKING
        rend.material.color = perfectColor;
        Debug.Log("PERFECT PARKING!");
        GameOverManager.Instance.ShowSuccess(GameTimer.Instance.GetElapsedTime());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            rend.material.color = idleColor;
        }
    }

    private Bounds GetCarBounds(Transform carRoot)
    {
        Collider[] cols = carRoot.GetComponentsInChildren<Collider>();

        Bounds b = cols[0].bounds;

        foreach (var c in cols)
        {
            b.Encapsulate(c.bounds);
        }

        return b;
    }
}