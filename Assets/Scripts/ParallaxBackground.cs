using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform cameraTransform;

    [Header("Parallax Effect")]
    [Range(0f, 1f)]
    public float parallaxEffectX = 0.5f;

    private float startPos;

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        startPos = transform.position.x;
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Calculate distance the camera has moved relative to the effect multiplier
        float dist = cameraTransform.position.x * parallaxEffectX;

        // Apply the new position while keeping original Y and Z coordinates
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}