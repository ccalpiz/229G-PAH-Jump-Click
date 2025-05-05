using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform;

    void LateUpdate()
    {
        Vector3 newPosition = cameraTransform.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
