using UnityEngine;
using UnityEngine.Video;

public class VideoEndHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer; // The VideoPlayer component
    public Canvas videoCanvas; // The Canvas that contains the VideoPlayer
    public Canvas otherCanvas; // The Canvas to activate after the video ends

    void Start()
    {
        if (videoPlayer == null || videoCanvas == null || otherCanvas == null)
        {
            Debug.LogError("Assign all required components in the inspector.");
            return;
        }

        // Ensure the other canvas is initially inactive
        otherCanvas.gameObject.SetActive(false);

        // Subscribe to the VideoPlayer's loopPointReached event
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Disable the video canvas and VideoPlayer
        videoCanvas.gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(false);

        // Enable the other canvas
        otherCanvas.gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the script is destroyed
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
