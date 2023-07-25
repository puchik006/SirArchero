using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform player; // The player's Transform component
    public Vector2 canvasSquareSize; // The size of the square on the canvas
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement
    public float zOffset = -10f; // The offset on the Z-axis

    private void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the boundaries of the square on the canvas
            float minX = canvasSquareSize.x * -0.5f;
            float maxX = canvasSquareSize.x * 0.5f;
            float minZ = canvasSquareSize.y * -0.5f;
            float maxZ = canvasSquareSize.y * 0.5f;

            // Calculate the target position on the X and Y axes
            float targetX = Mathf.Clamp(player.position.x, minX, maxX);
            float targetY = transform.position.y;

            // Set the target position on the Z-axis with the offset
            float targetZ = player.position.z + zOffset;

            // Calculate the target position the camera should move towards
            Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

            // Smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
