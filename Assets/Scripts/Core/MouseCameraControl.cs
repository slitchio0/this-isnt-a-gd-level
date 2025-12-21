using UnityEngine;

public class MouseCameraControl : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Distance")]
    public float distance = 8f;

    [Header("Vertical Clamp")]
    public float minPitch = -30f;
    public float maxPitch = 75f;

    [Header("Rotation Speed")]
    public float baseMouseSpeed = 2.5f;

    [Header("Smoothing")]
    public float smoothSpeed = 10f;

    float yaw;
    float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (Time.timeScale == 0f)
            return;

        float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);

        float mouseX = Input.GetAxisRaw("Mouse X") * baseMouseSpeed * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * baseMouseSpeed * sensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position - rotation * Vector3.forward * distance;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(target.position);
    }
}
