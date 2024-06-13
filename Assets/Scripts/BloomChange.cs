using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Video; // Add this to use VideoPlayer
using System.Collections;

public class BloomChange : MonoBehaviour
{
    public Renderer cubeRenderer; // Reference to the cube's renderer
    public Volume cubeVolume; // Reference to the Volume component for the cube
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component

    public float colorTransitionDuration = 2.0f; // Duration for each color transition
    public float bloomPulseDuration = 2.0f; // Duration for each bloom pulse
    public Color[] colors; // Array of colors to transition through
    public float minBloomIntensity = 1.0f; // Minimum bloom intensity
    public float maxBloomIntensity = 2.0f; // Maximum bloom intensity

    private int currentColorIndex;
    private int nextColorIndex;
    private float colorTransitionProgress;
    private Material cubeMaterial;
    private Bloom bloomLayer;
    private float bloomPulseProgress;

    void Start()
    {
        if (cubeRenderer == null)
        {
            Debug.LogError("Cube Renderer not assigned.");
            return;
        }

        if (cubeVolume == null)
        {
            Debug.LogError("Volume not assigned.");
            return;
        }

        if (!cubeVolume.profile.TryGet(out bloomLayer))
        {
            Debug.LogError("Bloom not found in the Volume profile.");
            return;
        }

        if (colors.Length < 2)
        {
            Debug.LogError("Please assign at least two colors.");
            return;
        }

        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer not assigned.");
            return;
        }

        cubeMaterial = cubeRenderer.material;
        currentColorIndex = 0;
        nextColorIndex = 1;
        colorTransitionProgress = 0.0f;
        bloomPulseProgress = 0.0f;

        videoPlayer.loopPointReached += OnVideoEnd; // Add listener for video end

        StartCoroutine(ChangeColor());
        StartCoroutine(PulseBloom());
    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
            // Lerp between current color and next color
            colorTransitionProgress += Time.deltaTime / colorTransitionDuration;
            cubeMaterial.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], colorTransitionProgress);

            if (colorTransitionProgress >= 1.0f)
            {
                // Reset transition progress and update color indices
                colorTransitionProgress = 0.0f;
                currentColorIndex = nextColorIndex;
                nextColorIndex = (nextColorIndex + 1) % colors.Length;
            }

            yield return null; // Wait for the next frame
        }
    }

    IEnumerator PulseBloom()
    {
        while (true)
        {
            // Calculate bloom intensity pulse using a sine wave for smooth pulsing
            bloomPulseProgress += Time.deltaTime / bloomPulseDuration;
            float bloomIntensity = minBloomIntensity + (Mathf.Sin(bloomPulseProgress * Mathf.PI * 2) + 1) / 2 * (maxBloomIntensity - minBloomIntensity);
            bloomLayer.intensity.value = bloomIntensity;

            if (bloomPulseProgress >= 1.0f)
            {
                // Reset pulse progress
                bloomPulseProgress = 0.0f;
            }

            yield return null; // Wait for the next frame
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Deactivate the cube when the video finishes
        cubeRenderer.gameObject.SetActive(false);
        cubeVolume.gameObject.SetActive(false);

    }
}
