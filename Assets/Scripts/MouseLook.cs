using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity;
    public Transform playerBody; // Assign the parent Player GameObject here in the Inspector
    [SerializeField] private float _verticalRotationLimit; // Limits vertical look (prevents flipping upside down)

    [SerializeField] private float _xRotation;

    void Start()
    {
        // Lock cursor to center of screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate() // Use LateUpdate for smooth camera movement after character position is updated
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        // Vertical rotation (looking up and down)
        _xRotation -= mouseY;
        // Clamp the vertical rotation to prevent the camera from flipping over
        _xRotation = Mathf.Clamp(_xRotation, -_verticalRotationLimit, _verticalRotationLimit);

        // Apply vertical rotation to the Camera Holder itself
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        // Horizontal rotation (looking left and right)
        // Apply horizontal rotation to the main Player body object
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
