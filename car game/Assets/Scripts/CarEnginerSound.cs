using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    [Header("Engine Sound")]
    public AudioSource engineSound;

    [Header("Pitch")]
    public float idlePitch = 0.8f;
    public float drivingPitch = 1.2f;

    void Start()
    {
        // If not assigned, find it automatically
        if (engineSound == null)
        {
            engineSound = GetComponentInChildren<AudioSource>(true);
        }

        if (engineSound != null)
        {
            engineSound.playOnAwake = false;
            engineSound.loop = true;
            engineSound.pitch = idlePitch;
        }
    }

    void Update()
    {
        if (engineSound == null)
            return;

        bool moving =
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S);

        if (moving)
        {
            if (!engineSound.isPlaying)
                engineSound.Play();

            engineSound.pitch = Mathf.Lerp(
                engineSound.pitch,
                drivingPitch,
                Time.deltaTime * 5f
            );
        }
        else
        {
            engineSound.pitch = Mathf.Lerp(
                engineSound.pitch,
                idlePitch,
                Time.deltaTime * 5f
            );

            // Stop once pitch returns close to idle
            if (engineSound.pitch <= idlePitch + 0.02f &&
                engineSound.isPlaying)
            {
                engineSound.Stop();
            }
        }
    }
}