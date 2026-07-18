using UnityEngine;

public class MenuCarPath : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] waypoints;

    [Header("Movement")]
    public float moveSpeed = 8f;
    public float turnSpeed = 5f;
    public float waypointRadius = 2f;
    public bool loop = true;

    [Header("Wheel Meshes")]
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;

    [Header("Wheel Settings")]
    public float wheelRadius = 0.35f;
    public float maxSteerAngle = 30f;

    private int currentWaypoint = 0;

    private float wheelSpin = 0f;
    private float steerAngle = 0f;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypoint];

        Vector3 dir = target.position - transform.position;
        dir.y = 0f;

        if (dir.magnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                turnSpeed * Time.deltaTime);

            float angle =
                Vector3.SignedAngle(
                    transform.forward,
                    dir.normalized,
                    Vector3.up);

            steerAngle = Mathf.Lerp(
                steerAngle,
                Mathf.Clamp(angle, -maxSteerAngle, maxSteerAngle),
                Time.deltaTime * 5f);
        }

        Vector3 oldPos = transform.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime);

        float distance = Vector3.Distance(oldPos, transform.position);

        wheelSpin +=
            (distance / (2f * Mathf.PI * wheelRadius)) * 360f;

        ApplyWheelRotation();

        if (Vector3.Distance(transform.position, target.position) < waypointRadius)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
            {
                if (loop)
                    currentWaypoint = 0;
                else
                    enabled = false;
            }
        }
    }

    void ApplyWheelRotation()
    {
        if (frontLeftWheel)
            frontLeftWheel.localRotation =
                Quaternion.Euler(wheelSpin, steerAngle, 0);

        if (frontRightWheel)
            frontRightWheel.localRotation =
                Quaternion.Euler(wheelSpin, steerAngle, 0);

        if (rearLeftWheel)
            rearLeftWheel.localRotation =
                Quaternion.Euler(wheelSpin, 0, 0);

        if (rearRightWheel)
            rearRightWheel.localRotation =
                Quaternion.Euler(wheelSpin, 0, 0);
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null) return;

        Gizmos.color = Color.green;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (!waypoints[i]) continue;

            Gizmos.DrawSphere(waypoints[i].position, 0.3f);

            if (i < waypoints.Length - 1)
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            else if (loop)
                Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
        }
    }
}