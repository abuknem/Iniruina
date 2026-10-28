using UnityEngine;

public class FloatingMist : MonoBehaviour
{
    [Header("Floating Motion Settings")]
    [Tooltip("How wide the circular drifting motion is horizontally.")]
    public float circleWidth = 0.5f;

    [Tooltip("How tall the circular drifting motion is vertically.")]
    public float circleHeight = 0.3f;

    [Tooltip("How fast the object loops through the circular motion.")]
    public float circleSpeed = 2f;

    private Vector3 startPosition;
    private float timeOffset;

    private void Start()
    {
        // Remember the original starting position of the object
        startPosition = transform.position;

        // Give each object a unique random offset so multiple objects don't move in exact sync
        timeOffset = Random.Range(0f, 10f);
    }

    private void Update()
    {
        // Calculate the circular/oval offset using Sine and Cosine over time
        float time = (Time.time + timeOffset) * circleSpeed;
        float xOffset = Mathf.Cos(time) * circleWidth;
        float yOffset = Mathf.Sin(time) * circleHeight;

        // Apply the offset relative to the original position
        transform.position = startPosition + new Vector3(xOffset, yOffset, 0);
    }
}
