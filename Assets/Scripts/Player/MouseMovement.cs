using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 300f;
    float xRotation = 0.0f;
    float yRotation = 0.0f;

    public float topClamp = -90.0f;
    public float bottomClamp = 90.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Hide the cursor and lock it to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player around the X axis
        xRotation -= mouseY;

        // Clamp the rotation so the player can't look behind themselves
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Rotate the player around the Y axis
        yRotation += mouseX;

        // Rotate the player
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0.0f);

    }
}
