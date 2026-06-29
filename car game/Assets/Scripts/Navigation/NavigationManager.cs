using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance;

    [Header("Player")]
    public Transform player;

    [Header("Waypoints")]
    public Waypoint[] waypoints;

    [Header("UI")]
    public Image directionIcon;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI distanceText;

    [Header("Sprites")]
    public Sprite straightSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite parkingSprite;

    [Header("Blink")]
    public float blinkSpeed = 0.25f;

    private int currentWaypoint = 0;
    private bool instructionShown = false;
    private Coroutine blinkRoutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HideNavigation();
    }

    private void Update()
    {
        if (currentWaypoint >= waypoints.Length)
            return;

        Waypoint wp = waypoints[currentWaypoint];

        float distance = Vector3.Distance(
            player.position,
            wp.transform.position
        );

        //----------------------------------
        // SHOW NAVIGATION
        //----------------------------------

        if (!instructionShown && distance <= wp.showDistance)
        {
            ShowNavigation(wp);
        }

        //----------------------------------
        // UPDATE DISTANCE
        //----------------------------------

        if (instructionShown)
        {
            distanceText.text = Mathf.RoundToInt(distance) + " m";
        }

        //----------------------------------
        // WAYPOINT REACHED
        //----------------------------------

        if (distance <= wp.reachDistance)
        {
            currentWaypoint++;

            HideNavigation();

            if (currentWaypoint >= waypoints.Length)
            {
                instructionText.text = "";
                distanceText.text = "";
                directionIcon.enabled = false;
            }
        }
    }

    void ShowNavigation(Waypoint wp)
    {
        instructionShown = true;

        directionIcon.enabled = true;
        instructionText.enabled = true;
        distanceText.enabled = true;

        instructionText.text = wp.instruction;

        switch (wp.direction)
        {
            case Waypoint.DirectionType.Straight:
                directionIcon.sprite = straightSprite;
                break;

            case Waypoint.DirectionType.Left:
                directionIcon.sprite = leftSprite;
                break;

            case Waypoint.DirectionType.Right:
                directionIcon.sprite = rightSprite;
                break;

            case Waypoint.DirectionType.Destination:
                directionIcon.sprite = parkingSprite;
                break;
        }

        blinkRoutine = StartCoroutine(BlinkIcon());
    }

    void HideNavigation()
    {
        instructionShown = false;

        directionIcon.enabled = false;
        instructionText.enabled = false;
        distanceText.enabled = false;

        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);
    }

    IEnumerator BlinkIcon()
    {
        while (true)
        {
            directionIcon.enabled = !directionIcon.enabled;

            yield return new WaitForSecondsRealtime(blinkSpeed);
        }
    }
}