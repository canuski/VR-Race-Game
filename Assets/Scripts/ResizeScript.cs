using UnityEngine;

public class ResizeScript : MonoBehaviour
{
    public Canvas canvas;
    public Vector2 originalCanvasSize = new Vector2(1920, 1080);
    public Vector2 targetCanvasSize = new Vector2(32, 18);

    void Start()
    {
        ResizeUIElements();
    }

    void ResizeUIElements()
    {
        float widthRatio = targetCanvasSize.x / originalCanvasSize.x;
        float heightRatio = targetCanvasSize.y / originalCanvasSize.y;

        foreach (RectTransform rectTransform in canvas.GetComponentsInChildren<RectTransform>())
        {
            if (rectTransform == canvas.GetComponent<RectTransform>())
                continue; // Skip the canvas itself

            Vector2 originalSize = rectTransform.sizeDelta;
            rectTransform.sizeDelta = new Vector2(originalSize.x * widthRatio, originalSize.y * heightRatio);

            Vector3 originalPosition = rectTransform.localPosition;
            rectTransform.localPosition = new Vector3(originalPosition.x * widthRatio, originalPosition.y * heightRatio, originalPosition.z);
        }
    }
}
