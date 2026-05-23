using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashReset : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            
            GameOverManager.Instance.ShowCrash();
        }
    }
}