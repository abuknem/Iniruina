using UnityEngine;

public class ParallaxLooping : MonoBehaviour
{
    [Header("Target & Parallax Setup")]
    [Tooltip("Drag your Main Camera here.")]
    public Transform cameraTransform;

    [Tooltip("How much this layer moves with the camera (0 = static background, 1 = moves fully with camera like UI).")]
    public float parallaxEffectMultiplier = 0.5f;

    [Header("Infinite Loop Setup")]
    [Tooltip("Width of one background sprite/tile in Unity world units.")]
    public float textureWidth;

    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        lastCameraPosition = cameraTransform.position;

        // Automatically calculate width from SpriteRenderer if textureWidth is 0
        if (textureWidth <= 0)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                textureWidth = spriteRenderer.bounds.size.x;
            }
        }
    }

    private void LateUpdate()
    {
        // 1. Calculate camera movement since last frame
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // 2. Apply Parallax effect
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y * parallaxEffectMultiplier, 0);
        lastCameraPosition = cameraTransform.position;

        // 3. Infinite Looping Check
        // Checks distance between camera and background segment
        float cameraDistanceFromObject = cameraTransform.position.x - transform.position.x;

        // If camera moves too far right, reposition layer to the right
        if (cameraDistanceFromObject >= textureWidth)
        {
            transform.position += new Vector3(textureWidth * 2f, 0, 0);
        }
        // If camera moves too far left, reposition layer to the left
        else if (cameraDistanceFromObject <= -textureWidth)
        {
            transform.position -= new Vector3(textureWidth * 2f, 0, 0);
        }
    }
}
