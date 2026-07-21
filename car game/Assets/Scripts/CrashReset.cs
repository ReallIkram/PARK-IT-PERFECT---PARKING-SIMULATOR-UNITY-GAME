using UnityEngine;

public class CrashReset : MonoBehaviour
{
    private CarEngineSound engineSound;

    private void Start()
    {
        engineSound = GetComponent<CarEngineSound>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform t = collision.transform;

        while (t != null)
        {
            if (t.CompareTag("Obstacle"))
            {
                // Stop the engine sound immediately
                if (engineSound != null)
                {
                    engineSound.StopEngineSound();
                }

                // Show crash UI
                GameOverManager.Instance.ShowCrash();

                return;
            }

            t = t.parent;
        }
    }
}