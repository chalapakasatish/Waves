using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Drag PlayerMovement here
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0f, 15f, -10f); // Height & Distance

    private void LateUpdate()
    {
        if (target == null) return;

        // Desired follow position
        Vector3 desiredPosition = target.position + offset;

        // Smooth camera movement
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        // Keep camera looking at the player (optional)
        transform.LookAt(target);
    }
}
