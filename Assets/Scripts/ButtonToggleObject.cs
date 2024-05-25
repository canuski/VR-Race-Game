using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonToggleObjects : MonoBehaviour
{
    public List<GameObject> targetObjects; // List of GameObjects to toggle

    private Button button; // The Button component

    void Start()
    {
        // Get the Button component attached to this GameObject
        button = GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError("Button component is missing.");
            return;
        }

        if (targetObjects == null || targetObjects.Count == 0)
        {
            Debug.LogError("Target GameObjects are not assigned.");
            return;
        }

        // Add a listener to the button's onClick event
        button.onClick.AddListener(ToggleObjects);
    }

    void ToggleObjects()
    {
        // Toggle the active state of each target GameObject
        foreach (var targetObject in targetObjects)
        {
            if (targetObject != null)
            {
                targetObject.SetActive(!targetObject.activeSelf);
            }
        }
    }
}
