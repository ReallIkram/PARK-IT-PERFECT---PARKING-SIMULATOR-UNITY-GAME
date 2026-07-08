using UnityEngine;

public class TrafficAI : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float turnSpeed = 3f;
    [SerializeField] private float waypointThreshold = 2f;

    [Header("Obstacle Avoidance")]
    [SerializeField] private float detectionDistance = 8f;
    [SerializeField] private float sideDetectionDistance = 5f;
    [SerializeField] private float rayHeightOffset = 0.5f;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private float avoidanceSteerStrength = 2f;
    [SerializeField] private float brakingSpeedFactor = 0.2f; // speed multiplier when blocked

    private int currentWaypoint = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypoint];
        Vector3 toWaypoint = (target.position - transform.position);
        toWaypoint.y = 0;

        // Base steering towards waypoint
        Quaternion targetRotation = Quaternion.LookRotation(toWaypoint.normalized);

        // --- Obstacle detection ---
        Vector3 origin = transform.position + Vector3.up * rayHeightOffset;
        Vector3 forward = transform.forward;
        Vector3 rightRay = Quaternion.Euler(0, 25, 0) * forward;
        Vector3 leftRay = Quaternion.Euler(0, -25, 0) * forward;

        bool centerBlocked = Physics.Raycast(origin, forward, out RaycastHit centerHit, detectionDistance, obstacleLayers);
        bool rightBlocked = Physics.Raycast(origin, rightRay, out RaycastHit rightHit, sideDetectionDistance, obstacleLayers);
        bool leftBlocked = Physics.Raycast(origin, leftRay, out RaycastHit leftHit, sideDetectionDistance, obstacleLayers);

        float currentSpeed = moveSpeed;
        Vector3 steerDirection = toWaypoint.normalized;

        if (centerBlocked)
        {
            // Something directly ahead - slow down heavily
            currentSpeed *= brakingSpeedFactor;

            // Steer away from whichever side is more open
            if (!leftBlocked && rightBlocked)
            {
                steerDirection = Quaternion.Euler(0, -avoidanceSteerStrength * 20f, 0) * forward;
            }
            else if (!rightBlocked && leftBlocked)
            {
                steerDirection = Quaternion.Euler(0, avoidanceSteerStrength * 20f, 0) * forward;
            }
            else if (!leftBlocked && !rightBlocked)
            {
                // Both sides open, pick either (default right)
                steerDirection = Quaternion.Euler(0, avoidanceSteerStrength * 20f, 0) * forward;
            }
            else
            {
                // Boxed in on all sides - stop
                currentSpeed = 0f;
            }
        }
        else if (rightBlocked)
        {
            steerDirection = Quaternion.Euler(0, -avoidanceSteerStrength * 10f, 0) * forward;
        }
        else if (leftBlocked)
        {
            steerDirection = Quaternion.Euler(0, avoidanceSteerStrength * 10f, 0) * forward;
        }

        // Apply rotation (blend waypoint direction with avoidance steering)
        Quaternion finalRotation = Quaternion.LookRotation(steerDirection);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, finalRotation, turnSpeed * Time.fixedDeltaTime));

        // Apply movement
        rb.MovePosition(rb.position + transform.forward * currentSpeed * Time.fixedDeltaTime);

        // Waypoint switching
        if (toWaypoint.magnitude < waypointThreshold)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    // Visualize rays in Scene view for debugging
    void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + Vector3.up * rayHeightOffset;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, transform.forward * detectionDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, Quaternion.Euler(0, 25, 0) * transform.forward * sideDetectionDistance);
        Gizmos.DrawRay(origin, Quaternion.Euler(0, -25, 0) * transform.forward * sideDetectionDistance);
    }
}