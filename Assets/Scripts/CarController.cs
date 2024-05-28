using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private PlayerInput inputManager;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
    public float speed = 500f;
    public float strengthCoefficient = 20000f;
    public float maxTurn = 20f;

    void Start()
    {
        inputManager = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        foreach (WheelCollider wheel in throttleWheels)
        {
            if (inputManager.Acceleration > 0) // Forward
            {
                wheel.motorTorque = strengthCoefficient * Time.deltaTime * inputManager.Acceleration * speed;
            }
            else if (inputManager.Reverse > 0) // Reverse
            {
                wheel.motorTorque = -strengthCoefficient * Time.deltaTime * inputManager.Reverse * speed;
            }
            else
            {
                wheel.motorTorque = 0;
            }

            // Set wheel damping rate
            wheel.wheelDampingRate = inputManager.WheelDampening;
        }

        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * inputManager.Steering;
        }
    }
}
