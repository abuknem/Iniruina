using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Target Setup")]
    [Tooltip("Drag your Main Camera here. Leave blank to auto-find Camera.main.")]
    public Transform cameraTransform;

    [Header("Parallax Settings")]
    [Tooltip("How much this background group moves relative to the camera (0 = static, 1 = moves 1:1 with camera).")]
    [Range(0f, 1f)]
    public float parallaxEffectMultiplier = 0.5f;

    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (cameraTransform == null)
        {
            if (Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
            else
            {
                Debug.LogError("ParentParallax: No Main Camera found in the scene!");
                enabled = false;
                return;
            }
        }

        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        // Calculate how far the camera moved since the last frame
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Move this parent object (and by extension, all its children) by the parallax multiplier
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y * parallaxEffectMultiplier, 0);

        // Update the last camera position for the next frame
        lastCameraPosition = cameraTransform.position;
    }
}
