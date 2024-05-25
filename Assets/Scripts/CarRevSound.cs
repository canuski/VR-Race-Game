using System.Collections;
using UnityEngine;

public class CarRevSound : MonoBehaviour
{
    public AudioClip revSound; // Assign this in the Inspector
    public AudioClip continuousSound; // Assign this in the Inspector
    public AudioClip tailSound; // Assign this in the Inspector
    public AudioSource targetAudioSource; // Assign the specific AudioSource in the Inspector

    private bool isRevSoundPlaying = false;
    private bool isContinuousSoundPlaying = false;

    void Start()
    {
        if (targetAudioSource == null)
        {
            Debug.LogWarning("Target AudioSource is not assigned.");
        }
    }

    void Update()
    {
        // Check if the primary index trigger on the right Oculus Touch controller is pressed
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (!isRevSoundPlaying && !isContinuousSoundPlaying)
            {
                StartCoroutine(PlaySounds());
            }
        }
        // Check if the trigger is released
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            StopContinuousSound();
        }
    }

    IEnumerator PlaySounds()
    {
        if (revSound != null && targetAudioSource != null)
        {
            isRevSoundPlaying = true;
            targetAudioSource.PlayOneShot(revSound);

            // Calculate overlap time (e.g., 0.1 seconds before the rev sound ends)
            float overlapTime = 0.5f;
            yield return new WaitForSeconds(revSound.length - overlapTime);

            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                PlayContinuousSound();
            }

            yield return new WaitForSeconds(overlapTime); // Wait for the rest of the rev sound duration
            isRevSoundPlaying = false;
        }
        else
        {
            Debug.LogWarning("Target AudioSource or revSound is not assigned.");
        }
    }

    void PlayContinuousSound()
    {
        if (continuousSound != null && targetAudioSource != null)
        {
            isContinuousSoundPlaying = true;
            targetAudioSource.loop = true;
            targetAudioSource.clip = continuousSound;
            targetAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Target AudioSource or continuousSound is not assigned.");
        }
    }

    void StopContinuousSound()
    {
        if (isContinuousSoundPlaying && targetAudioSource != null)
        {
            isContinuousSoundPlaying = false;
            StartCoroutine(PlayTailSound());
        }
    }

    IEnumerator PlayTailSound()
    {
        if (tailSound != null && targetAudioSource != null)
        {
            // Adjust the overlap time to make the transition smoother
            float overlapTime = tailSound.length * 0.5f; // Overlap by 10% of the tail sound length

            // Start the tail sound just before stopping the continuous sound
            targetAudioSource.loop = false;

            yield return new WaitForSeconds(overlapTime);

            targetAudioSource.PlayOneShot(tailSound);

            // Stop the continuous sound just after starting the tail sound to ensure overlap
            yield return new WaitForSeconds(overlapTime);

            targetAudioSource.Stop();
        }
        else
        {
            Debug.LogWarning("Target AudioSource or tailSound is not assigned.");
        }
    }
}
