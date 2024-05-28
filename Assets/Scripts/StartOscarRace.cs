using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public AudioClip countdownBeep;
    public AudioClip startBeep;
    public float countdownTime = 8f;

    private AudioSource audioSource;
    private float timeRemaining;

    public PlayerInput playerInput;
    public CarController carController;
    public KartController[] kartControllers;

    // Audio source for the rev sound
    public AudioSource revSoundSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        timeRemaining = countdownTime;

        // Freeze the game at the start
        FreezeGame(true);

        // Disable rev sound at the start
        if (revSoundSource != null)
        {
            revSoundSource.Stop();
        }

        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        while (timeRemaining > 0)
        {
            PlayBeep(countdownBeep);
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }

        PlayBeep(startBeep);
        StartGame();
    }

    void PlayBeep(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void StartGame()
    {
        // Unfreeze the game
        FreezeGame(false);

        // Enable rev sound when the game starts
        if (revSoundSource != null)
        {
            revSoundSource.Play();
        }

        // Implement any additional logic to start the game
        Debug.Log("Game Started!");
    }

    void FreezeGame(bool freeze)
    {
        // Enable/Disable the PlayerInput and CarController scripts
        playerInput.enabled = !freeze;
        carController.enabled = !freeze;

        // Enable/Disable all KartController scripts
        foreach (var kartController in kartControllers)
        {
            kartController.enabled = !freeze;
        }

        Debug.Log($"Freeze game: {freeze}");
        Debug.Log($"PlayerInput enabled: {playerInput.enabled}");
        Debug.Log($"CarController enabled: {carController.enabled}");
        Debug.Log($"KartControllers enabled: {!freeze}");
    }
}
