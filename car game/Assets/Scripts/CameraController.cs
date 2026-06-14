using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Anchors — drag from Inspector")]
    public Transform anchorTPP;
    public Transform anchorTop;
    public Transform anchorFPP;

    [Header("Settings")]
    public KeyCode switchKey = KeyCode.C;
    public float lerpSpeed = 5f;      // transition smoothness

    enum CamMode { TPP, Top, FPP }
    CamMode currentMode = CamMode.TPP;

    Transform targetAnchor;

    void Start()
    {
        targetAnchor = anchorTPP;
        SnapToTarget();              // start without animation
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            CycleMode();
        }
        SmoothFollow();
    }

    void CycleMode()
    {
        currentMode = (CamMode)(((int)currentMode + 1) % 3);

        switch (currentMode)
        {
            case CamMode.TPP: targetAnchor = anchorTPP; break;
            case CamMode.Top: targetAnchor = anchorTop; break;
            case CamMode.FPP: targetAnchor = anchorFPP; break;
        }

        Debug.Log("Camera: " + currentMode);
    }

    void SmoothFollow()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetAnchor.position,
            Time.deltaTime * lerpSpeed
        );
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetAnchor.rotation,
            Time.deltaTime * lerpSpeed
        );
    }

    void SnapToTarget()
    {
        transform.SetPositionAndRotation(
            targetAnchor.position,
            targetAnchor.rotation
        );
    }
}
