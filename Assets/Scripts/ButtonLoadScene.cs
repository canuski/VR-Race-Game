using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLoadScene : MonoBehaviour
{
    public string sceneName; // The name of the scene to load

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

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is not assigned.");
            return;
        }

        // Add a listener to the button's onClick event
        button.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}
