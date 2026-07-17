using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource uiAudioSource;
    public AudioClip hoverSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (uiAudioSource != null && hoverSound != null)
        {
            uiAudioSource.PlayOneShot(hoverSound);
        }
    }
}