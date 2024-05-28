using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CheckpointTrigger : MonoBehaviour
{
    public string targetTag = "Player"; // Tag of the car GameObject
    private int passCount = 0;
    public int passThreshold = 2; // Number of passes required to change the scene
    public string sceneToLoad = "LevelMohammed"; // Name of the scene to load
    public TextMeshProUGUI lapText; // Reference to the TextMeshProUGUI component

    private void Start()
    {
        UpdateLapText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            passCount++;
            Debug.Log($"Checkpoint passed: {passCount} times");

            UpdateLapText();

            if (passCount >= passThreshold)
            {
                Debug.Log("Loading next scene...");
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    private void UpdateLapText()
    {
        int currentLap = Mathf.Min(passCount + 1, passThreshold); // Ensure it doesn't go beyond the threshold
        lapText.text = $"Lap: {currentLap}";
    }
}
