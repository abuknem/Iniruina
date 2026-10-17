using UnityEngine;
using UnityEngine.InputSystem;

public class IntBoxDrop : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private bool isPlayerNearby = false;
    private bool hasDropped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    void Update()
    {
        if (isPlayerNearby && !hasDropped && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            DropBox();
        }
    }

    private void DropBox()
    {
        hasDropped = true;
        transform.SetParent(null);

        if (col != null)
        {
            col.isTrigger = false;
        }

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 3f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasDropped && collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!hasDropped && collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
