using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour
{
    public Light areaLight; // Reference to the Area Light
    public float duration = 2.0f; // Duration for each color transition
    public Color[] colors; // Array of colors to transition through
    public float minIntensity = 0.5f; // Minimum intensity of the light
    public float maxIntensity = 1.5f; // Maximum intensity of the light

    private int currentColorIndex;
    private int nextColorIndex;
    private float transitionProgress;
    private float currentIntensity;
    private float nextIntensity;

    void Start()
    {
        if (areaLight == null)
        {
            Debug.LogError("Area Light not assigned.");
            return;
        }

        if (colors.Length < 2)
        {
            Debug.LogError("Please assign at least two colors.");
            return;
        }

        currentColorIndex = 0;
        nextColorIndex = 1;
        transitionProgress = 0.0f;
        currentIntensity = Random.Range(minIntensity, maxIntensity);
        nextIntensity = Random.Range(minIntensity, maxIntensity);
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
            // Lerp between current color and next color
            transitionProgress += Time.deltaTime / duration;
            areaLight.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], transitionProgress);
            areaLight.intensity = Mathf.Lerp(currentIntensity, nextIntensity, transitionProgress);

            if (transitionProgress >= 1.0f)
            {
                // Reset transition progress and update color and intensity indices
                transitionProgress = 0.0f;
                currentColorIndex = nextColorIndex;
                nextColorIndex = (nextColorIndex + 1) % colors.Length;
                currentIntensity = nextIntensity;
                nextIntensity = Random.Range(minIntensity, maxIntensity);
            }

            yield return null; // Wait for the next frame
        }
    }
}
