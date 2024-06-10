using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dirk Dynamite/CameraAnchor")]
[DisallowMultipleComponent]

public class CameraAnchor : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerBody;

    void Update()
    {
        Vector2 desiredPosition = playerBody.position + playerBody.velocity;

        desiredPosition.y = Mathf.Clamp(desiredPosition.y, playerBody.position.y - 0.3f, playerBody.position.y + 0.3f);
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, playerBody.position.x - 0.5f, playerBody.position.x + 0.5f);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Mathf.SmoothStep(0f, 1f, 0.1f));
    }
}