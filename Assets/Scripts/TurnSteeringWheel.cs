using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    public WheelCollider frontLeftWheel; // The WheelCollider for steering
    public Transform steeringWheel; // The steering wheel transform

    public float maxWheelSteerAngle = 30f; // Maximum steering angle for the wheels
    public float maxSteeringWheelAngle = 360f; // Maximum steering wheel rotation in degrees
    public float steeringSmoothness = 0.1f; // Smoothness factor for lerp

    private Quaternion initialSteeringWheelRotation;

    void Start()
    {
        // Capture the initial steering wheel rotation
        initialSteeringWheelRotation = steeringWheel.localRotation;
    }

    void Update()
    {
        if (frontLeftWheel == null || steeringWheel == null)
        {
            Debug.LogError("WheelCollider or Steering Wheel reference is missing!");
            return;
        }

        // Get the current steering angle from the WheelCollider
        float currentSteeringAngle = frontLeftWheel.steerAngle;

        // Map the steering angle to the steering wheel's rotation
        float targetSteeringWheelZRotation = (currentSteeringAngle / maxWheelSteerAngle) * maxSteeringWheelAngle;

        // Create the target rotation based on the calculated angle
        Quaternion targetRotation = Quaternion.Euler(0, 0, -targetSteeringWheelZRotation);

        // Smoothly interpolate from the current rotation to the target rotation
        Quaternion smoothedRotation = Quaternion.Lerp(steeringWheel.localRotation, initialSteeringWheelRotation * targetRotation, steeringSmoothness);

        // Apply the smoothed rotation to the steering wheel
        steeringWheel.localRotation = smoothedRotation;
    }
}
