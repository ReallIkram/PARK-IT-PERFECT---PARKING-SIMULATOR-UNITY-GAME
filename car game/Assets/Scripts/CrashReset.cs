using UnityEngine;

public class CrashReset : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform t = collision.transform;

        while (t != null)
        {
            if (t.CompareTag("Obstacle"))
            {
                GameOverManager.Instance.ShowCrash();
                return;
            }

            t = t.parent;
        }
    }
}