using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Acceleration
    {
        get { return m_Acceleration; }
    }
    public float Steering
    {
        get { return m_Steering; }
    }
    public float Reverse
    {
        get { return m_Reverse; }
    }

    float m_Acceleration;
    float m_Steering;
    float m_Reverse;

    bool m_FixedUpdateHappend;

    private bool accelerating = false;
    private bool reversing = false;
    private bool breaking = false;
    private bool turningLeft = false;
    private bool turningRight = false;

    public float wheelDampening;

    private void Update()
    {
        GetPlayerInput();

        if (accelerating)
        {
            m_Acceleration = 1f;
            wheelDampening = 500f;
        }
        else if (breaking)
        {
            m_Acceleration = -0.5f;
            wheelDampening = 10000f;
        }
        else if (reversing)
        {
            m_Reverse = 1f;
            wheelDampening = 500f;
        }
        else
        {
            m_Acceleration = 0f;
            wheelDampening = 5f;
        }

        if (turningLeft)
        {
            m_Steering = -1f;

        }
        else if (!turningLeft && turningRight)
        {
            m_Steering = 1f;
        }
        else
        {
            m_Steering = 0f;
        }
    }
    private void GetPlayerInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            accelerating = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            accelerating = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            reversing = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            reversing = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
        {
            breaking = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
        {
            breaking = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch))
        {
            turningLeft = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch))
        {
            turningLeft = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch))
        {
            turningRight = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch))
        {
            turningRight = false;
        }
    }
}