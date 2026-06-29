// using UnityEngine;
// using UnityEngine.UI;

// public class NavigationManager : MonoBehaviour
// {
//     public static NavigationManager Instance;

//     [Header("References")]
//     public Transform player;
//     public Transform destination;

//     [Header("UI")]
//     public RectTransform arrow;

//     private void Awake()
//     {
//         Instance = this;
//     }

//     private void Update()
//     {
//         UpdateArrow();
//     }

//     void UpdateArrow()
//     {
//         Vector3 direction = destination.position - player.position;

//         direction.y = 0;

//         float angle = Vector3.SignedAngle(
//             player.forward,
//             direction,
//             Vector3.up
//         );

//         arrow.localEulerAngles = new Vector3(0, 0, -angle);
//     }
// }