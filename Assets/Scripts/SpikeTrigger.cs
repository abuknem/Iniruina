using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Teleport the player to coordinates (0, 0)
            collision.transform.position = new Vector3(0f, 0f, 0f);

            // Optional: If your player uses a Rigidbody2D, you might want to reset its velocity 
            // so they don't keep carrying momentum after teleporting:
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero; // Use .velocity if you are on an older Unity version
            }
        }
    }
}
