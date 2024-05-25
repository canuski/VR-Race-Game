using UnityEngine;
using UnityEngine.UI;

public class ButtonQuitGame : MonoBehaviour
{
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

        // Add a listener to the button's onClick event
        button.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        // Quit the application
#if UNITY_EDITOR
        // If running in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running in a standalone build
        Application.Quit();
#endif
    }
}
