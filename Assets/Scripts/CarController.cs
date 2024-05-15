using System.Collections.Generic;
using UnityEngine;

public class CarCOntroller : MonoBehaviour
{

    private PlayerInput inputManager;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
    public float speed = 1200f;
    public float strengthCoefficient = 200000f;
    public float maxTurn = 20f;

    void Start()
    {
        inputManager = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
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
            wheel.wheelDampingRate = inputManager.wheelDampening;
        }


        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * inputManager.Steering;
            wheel.wheelDampingRate *= inputManager.wheelDampening;
        }
    }
}
