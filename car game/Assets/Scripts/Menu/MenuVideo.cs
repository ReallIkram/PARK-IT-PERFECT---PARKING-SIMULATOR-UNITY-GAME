using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class MenuVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "bgvideo_unity.mp4");
        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }
}