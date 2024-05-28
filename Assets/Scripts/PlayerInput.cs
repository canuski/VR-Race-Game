using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Acceleration { get; private set; }
    public float Steering { get; private set; }
    public float Reverse { get; private set; }
    public float WheelDampening { get; private set; }

    private bool accelerating = false;
    private bool reversing = false;
    private bool braking = false;
    private bool turningLeft = false;
    private bool turningRight = false;

    private void Update()
    {
        GetPlayerInput();

        // Debug input states
        Debug.Log($"Accelerating: {accelerating}");
        Debug.Log($"Reversing: {reversing}");
        Debug.Log($"Braking: {braking}");
        Debug.Log($"Turning Left: {turningLeft}");
        Debug.Log($"Turning Right: {turningRight}");

        // Manage acceleration and braking states
        if (accelerating)
        {
            Acceleration = 1f;
            Reverse = 0f;
            WheelDampening = 500f;
        }
        else if (braking)
        {
            Acceleration = -0.5f;
            Reverse = 0f;
            WheelDampening = 10000f;
        }
        else if (reversing)
        {
            Acceleration = 0f;
            Reverse = 1f;
            WheelDampening = 500f;
        }
        else
        {
            Acceleration = 0f;
            Reverse = 0f;
            WheelDampening = 5f;
        }

        // Manage steering states
        if (turningLeft)
        {
            Steering = -1f;
        }
        else if (turningRight)
        {
            Steering = 1f;
        }
        else
        {
            Steering = 0f;
        }

        // Debug resulting values
        Debug.Log($"Acceleration: {Acceleration}");
        Debug.Log($"Reverse: {Reverse}");
        Debug.Log($"Steering: {Steering}");
        Debug.Log($"Wheel Dampening: {WheelDampening}");
    }

    private void GetPlayerInput()
    {
        // Update input states based on controller input
        accelerating = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        reversing = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        braking = OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch);
        turningLeft = OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch);
        turningRight = OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch);

        // Debug input detection
        Debug.Log("Inputs detected:");
        Debug.Log($"PrimaryIndexTrigger: {OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)}");
        Debug.Log($"PrimaryHandTrigger: {OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch)}");
        Debug.Log($"PrimaryThumbstickDown: {OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch)}");
        Debug.Log($"PrimaryThumbstickLeft: {OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch)}");
        Debug.Log($"PrimaryThumbstickRight: {OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch)}");
    }
}
