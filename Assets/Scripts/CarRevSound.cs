using System.Collections;
using UnityEngine;

public class CarRevSound : MonoBehaviour
{
    public AudioClip revSound; // Assign this in the Inspector
    public AudioClip continuousSound; // Assign this in the Inspector
    public AudioClip tailSound; // Assign this in the Inspector
    public AudioSource targetAudioSource; // Assign the specific AudioSource in the Inspector

    private Coroutine playSoundsCoroutine;

    void Start()
    {
        if (targetAudioSource == null)
        {
            Debug.LogWarning("Target AudioSource is not assigned.");
        }
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (playSoundsCoroutine == null)
            {
                playSoundsCoroutine = StartCoroutine(PlaySounds());
            }
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (playSoundsCoroutine != null)
            {
                StopCoroutine(playSoundsCoroutine);
                playSoundsCoroutine = null;
                StartCoroutine(PlayTailSound());
            }
        }
    }

    IEnumerator PlaySounds()
    {
        if (revSound != null && targetAudioSource != null)
        {
            // Play the rev sound
            targetAudioSource.PlayOneShot(revSound);

            // Calculate overlap time (e.g., 0.1 seconds before the rev sound ends)
            float overlapTime = 0.5f;
            yield return new WaitForSeconds(revSound.length - overlapTime);

            // Check if the trigger is still held down
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                PlayContinuousSound();
            }

            // Wait for the rest of the rev sound duration
            yield return new WaitForSeconds(overlapTime);

            // If the trigger is released, play the tail sound
            if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                StartCoroutine(PlayTailSound());
            }

            playSoundsCoroutine = null;
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
            targetAudioSource.loop = true;
            targetAudioSource.clip = continuousSound;
            targetAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Target AudioSource or continuousSound is not assigned.");
        }
    }

    IEnumerator PlayTailSound()
    {
        if (tailSound != null && targetAudioSource != null)
        {
            // Play the tail sound
            targetAudioSource.PlayOneShot(tailSound);

            // Calculate the duration to wait
            float tailSoundDuration = tailSound.length;

            // Stop the continuous sound
            if (targetAudioSource.isPlaying && targetAudioSource.clip == continuousSound)
            {
                targetAudioSource.loop = false;
                targetAudioSource.Stop();
            }

            // Wait for the tail sound to finish
            yield return new WaitForSeconds(tailSoundDuration);
        }
        else
        {
            Debug.LogWarning("Target AudioSource or tailSound is not assigned.");
        }
    }
}
