using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f); // Scale to increase to when hovering
    public float transitionSpeed = 5f; // Speed of the transition
    private Vector3 originalScale; // Store the original scale of the button
    private Vector3 targetScale; // The scale we are transitioning to
    private bool isHovered = false; // Flag to check if the button is hovered

    void Start()
    {
        // Store the original scale of the button
        originalScale = transform.localScale;
        // Set the initial target scale to the original scale
        targetScale = originalScale;
    }

    void Update()
    {
        // Smoothly transition to the target scale
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * transitionSpeed);
    }

    // Method called when the pointer enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set the target scale to the hover scale
        targetScale = hoverScale;
        isHovered = true;
    }

    // Method called when the pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        // Set the target scale back to the original scale
        targetScale = originalScale;
        isHovered = false;
    }
}
