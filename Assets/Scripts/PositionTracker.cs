using UnityEngine;
using TMPro;

public class PositionTracker : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Transform[] karts; // Array of all karts including the player
    public TextMeshProUGUI positionText; // Reference to the TextMeshProUGUI component

    void Update()
    {
        // Calculate the player's position
        int playerPosition = CalculatePlayerPosition();

        // Update the TextMeshPro component
        positionText.text = "Position: " + playerPosition;
    }

    int CalculatePlayerPosition()
    {
        // Calculate the player's current lap progress
        float playerProgress = GetKartProgress(player);

        // Count how many karts are ahead of the player
        int position = 1; // 1-based position
        foreach (var kart in karts)
        {
            if (kart != player)
            {
                if (GetKartProgress(kart) < playerProgress)
                {
                    position++;
                }
            }
        }

        return position;
    }

    float GetKartProgress(Transform kart)
    {
        // This method should return the progress of the kart on the track
        // It could be based on distance travelled, lap number, checkpoints, etc.
        // For simplicity, we use the z position (assuming a linear track)
        // You will need to replace this with your actual progress calculation
        return kart.position.z;
    }
}
