using UnityEngine;
using UnityEngine.Video;

public class UnityEventOnFinishVideoPlayer : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] VideoClip video;

    private void Update()
    {
        if (videoPlayer.time >= video.length)
            Debug.Log(videoPlayer.time);
    }
}
