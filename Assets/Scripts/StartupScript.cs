using UnityEngine;

public class StartupScript : MonoBehaviour
{
    void Awake()
    {
        // Set the target frame rate to 120Hz
        Application.targetFrameRate = 120;

        // You can also add other initializations here, like ensuring specific quality settings or initializing services
    }

    void Start()
    {
        // Any initialization that depends on objects in the scene or other components can go here

        Debug.Log("Startup script executed. Target frame rate set to 120Hz.");
    }
}
