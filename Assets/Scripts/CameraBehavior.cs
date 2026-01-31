using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public Transform player;           // Assign your player transform in the Inspector
    public float rightThreshold = 2f;  // How far from the center before the camera moves
    private float lastCameraX;

    void Start()
    {
        lastCameraX = transform.position.x;
    }

    void LateUpdate()
    {
        // Only follow if player is to the right of the threshold
        float cameraRightEdge = transform.position.x + rightThreshold;
        if (player.position.x > cameraRightEdge)
        {
            float newCameraX = player.position.x - rightThreshold;
            // Only move camera forward (never back)
            if (newCameraX > lastCameraX)
            {
                transform.position = new Vector3(newCameraX, transform.position.y, transform.position.z);
                lastCameraX = newCameraX;
            }
        }
    }
}
