using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarAudioController : MonoBehaviour
{
    [Header("References")]
    public PrometeoCarController car;

    [Header("Audio Sources")]
    public AudioSource idleSound;
    public AudioSource engineSound;
    public AudioSource reverseSound;
    public AudioSource brakeSound;
    public AudioSource handbrakeSound;
    public AudioSource crashSound;

    [Header("Engine")]
    public float minPitch = 0.8f;
    public float maxPitch = 2.2f;
    public float pitchSmooth = 5f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (car == null)
            car = GetComponent<PrometeoCarController>();

        if (idleSound)
        {
            idleSound.loop = true;
            idleSound.Play();
        }

        if (engineSound)
        {
            engineSound.loop = true;
            engineSound.volume = 0;
            engineSound.Play();
        }

        if (reverseSound)
        {
            reverseSound.loop = true;
            reverseSound.volume = 0;
            reverseSound.Play();
        }
    }

    void Update()
    {
        if (car == null)
            return;

        float speed = Mathf.Abs(car.carSpeed);

        float t = Mathf.Clamp01(speed / car.maxSpeed);

        if (idleSound)
            idleSound.pitch = Mathf.Lerp(0.9f, 1.15f, t);

        if (engineSound)
        {
            float targetPitch = Mathf.Lerp(minPitch, maxPitch, t);

            if (Input.GetKey(KeyCode.W))
                targetPitch += 0.2f;

            engineSound.pitch = Mathf.Lerp(engineSound.pitch, targetPitch, Time.deltaTime * pitchSmooth);

            float targetVolume = Input.GetKey(KeyCode.W) ? 1f : t * 0.5f;

            engineSound.volume = Mathf.Lerp(engineSound.volume, targetVolume, Time.deltaTime * 5f);
        }

        if (reverseSound)
        {
            float target = Input.GetKey(KeyCode.S) ? 0.8f : 0f;

            reverseSound.volume = Mathf.Lerp(reverseSound.volume, target, Time.deltaTime * 5f);

            reverseSound.pitch = Mathf.Lerp(0.9f, 1.3f, t);
        }

        if (brakeSound)
        {
            if (Input.GetKey(KeyCode.Space) && speed > 5)
            {
                if (!brakeSound.isPlaying)
                    brakeSound.Play();
            }
            else
            {
                brakeSound.Stop();
            }
        }

        if (handbrakeSound)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                handbrakeSound.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!crashSound)
            return;

        float impact = collision.relativeVelocity.magnitude;

        if (impact < 3f)
            return;

        crashSound.volume = Mathf.Clamp01(impact / 15f);
        crashSound.pitch = UnityEngine.Random.Range(0.95f, 1.05f);

        crashSound.Play();
    }
}