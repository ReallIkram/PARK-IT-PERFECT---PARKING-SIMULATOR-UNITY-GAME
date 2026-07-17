using UnityEngine;

public class MenuCarPath : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] waypoints;

    [Header("Movement")]
    public float moveSpeed = 8f;
    public float rotationSpeed = 5f;
    public float waypointRadius = 2f;
    public bool loop = true;

    private int currentWaypoint = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypoint];

        // Direction to target
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        // Rotate smoothly
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Move towards target
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        // Next waypoint
        if (Vector3.Distance(transform.position, target.position) <= waypointRadius)
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

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Gizmos.color = Color.green;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] == null) continue;

            Gizmos.DrawSphere(waypoints[i].position, 0.4f);

            if (i < waypoints.Length - 1)
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            else if (loop)
                Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
        }
    }
}