using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float sensitivity = 2f; // Mouse sensitivity

    void Update()
    {
        // Rotation
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.Rotate(Vector3.left * mouseY);

        // Only allow forward movement
        if (Input.GetKey(KeyCode.Z)) // Change KeyCode.W to KeyCode.Z
        {
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
