using UnityEngine;
using UnityEngine.InputSystem;

public class MouseOrbit : MonoBehaviour
{
    public Transform player;
    public float distance = 5.0f;
    public float mouseSensitivity = 0.2f;
    private float xRotation, yRotation;

    void Update()
    {
        if (player == null) return;

        Vector2 delta = Mouse.current.delta.ReadValue() * mouseSensitivity;
        xRotation += delta.x;
        yRotation += delta.y;
        yRotation = Mathf.Clamp(yRotation, -30f, 30f);

        Quaternion rotation = Quaternion.Euler(yRotation, xRotation, 0);
        transform.rotation = rotation;
        transform.position = player.position - (rotation * Vector3.forward * distance);
    }
}
