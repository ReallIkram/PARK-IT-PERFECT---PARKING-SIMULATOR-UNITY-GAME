using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    private AudioSource engine;

    [Header("Volume")]
    public float idleVolume = 0.25f;
    public float driveVolume = 1.5f;

    [Header("Pitch")]
    public float idlePitch = 0.9f;
    public float drivePitch = 1.8f;

    [Header("Smooth Speed")]
    public float smooth = 5f;

    private bool engineStopped = false;

    void Start()
    {
        engine = GetComponent<AudioSource>();

        if (engine == null)
        {
            Debug.LogError("No AudioSource found!");
            enabled = false;
            return;
        }

        engine.loop = true;

        if (!engine.isPlaying)
            engine.Play();

        engine.volume = idleVolume;
        engine.pitch = idlePitch;
    }

    void Update()
    {
        // Don't update the sound after it has been stopped
        if (engineStopped)
            return;

        bool accelerating =
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S);

        float targetVolume = accelerating ? driveVolume : idleVolume;
        float targetPitch = accelerating ? drivePitch : idlePitch;

        engine.volume = Mathf.Lerp(
            engine.volume,
            targetVolume,
            smooth * Time.deltaTime);

        engine.pitch = Mathf.Lerp(
            engine.pitch,
            targetPitch,
            smooth * Time.deltaTime);
    }

    // Call this when the car crashes
    public void StopEngineSound()
    {
        if (engine == null)
            return;

        engineStopped = true;
        engine.Stop();
    }
}