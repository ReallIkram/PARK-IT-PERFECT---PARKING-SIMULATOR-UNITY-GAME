using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public enum DirectionType
    {
        Straight,
        Left,
        Right,
        Destination
    }

    [Header("Navigation")]
    public DirectionType direction;

    [TextArea]
    public string instruction;

    [Header("Distance Settings")]
    public float showDistance = 25f;     // Show UI when player is this close
    public float reachDistance = 5f;     // Waypoint completed here
}