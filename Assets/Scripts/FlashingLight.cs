using UnityEngine;
using System.Collections;

public class FlashingLightsController : MonoBehaviour
{
    public GameObject[] flashingLightSpheres; // Array of spheres that flash
    public GameObject[] greenLightSpheres; // Array of spheres with green lights
    public float flashInterval = 2f; // Interval between flashes
    public int flashCount = 8; // Number of flashes before turning on the green lights

    void Start()
    {
        // Initially turn off all green lights and flashing lights
        foreach (GameObject greenLightSphere in greenLightSpheres)
        {
            greenLightSphere.SetActive(false);
        }

        foreach (GameObject flashingLightSphere in flashingLightSpheres)
        {
            flashingLightSphere.SetActive(false);
        }

        // Start the flashing light routine
        StartCoroutine(FlashLightRoutine());
    }

    IEnumerator FlashLightRoutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            foreach (GameObject flashingLightSphere in flashingLightSpheres)
            {
                flashingLightSphere.SetActive(true);
            }
            yield return new WaitForSeconds(flashInterval / 2); // Light is on for half the interval
            foreach (GameObject flashingLightSphere in flashingLightSpheres)
            {
                flashingLightSphere.SetActive(false);
            }
            yield return new WaitForSeconds(flashInterval / 2); // Light is off for half the interval
        }

        // Ensure all flashing lights are off after flashing
        foreach (GameObject flashingLightSphere in flashingLightSpheres)
        {
            flashingLightSphere.SetActive(false);
        }

        // Turn on all green lights
        TurnOnGreenLights();
    }

    void TurnOnGreenLights()
    {
        foreach (GameObject greenLightSphere in greenLightSpheres)
        {
            greenLightSphere.SetActive(true);
        }
    }
}
