using UnityEngine;

public class VerticalCameraFollow : MonoBehaviour
{
    public Transform player;         // Drag your player (Emily) into this field in the Inspector
    public float smoothSpeed = 0.2f; // Adjust this to make camera more or less smooth

    private float currentY;

    void Start()
    {
        if (player != null)
        {
            currentY = player.position.y;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        float targetY = player.position.y;
        currentY = Mathf.Lerp(currentY, targetY, smoothSpeed);

        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }
}
