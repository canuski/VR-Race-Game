using System.Collections.Generic;
using UnityEngine;

public class CarCOntroller : MonoBehaviour
{

    private PlayerInput inputManager;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
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
            wheel.motorTorque = strengthCoefficient * Time.deltaTime * inputManager.Acceleration * 1000;
            wheel.wheelDampingRate = inputManager.wheelDampening;
        }

        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * inputManager.Steering;
            wheel.wheelDampingRate *= inputManager.wheelDampening;
        }
    }
}
