using UnityEngine;
using UnityEngine.InputSystem;

public class RopeCutInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject boxToDrop;
    [SerializeField] private Joint2D jointToBreak;
    [SerializeField] private bool destroyRopeOnCut = true;

    private bool playerInRange = false;
    private bool isCut = false;

    private void Update()
    {
        // Check if the new Input System keyboard is active and 'E' was pressed
        if (playerInRange && !isCut && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            CutRope();
        }
    }

    private void CutRope()
    {
        isCut = true;

        if (boxToDrop != null)
        {
            Rigidbody2D boxRb = boxToDrop.GetComponent<Rigidbody2D>();
            if (boxRb != null)
            {
                boxRb.bodyType = RigidbodyType2D.Dynamic;
            }
            boxToDrop.transform.SetParent(null);
        }

        if (jointToBreak != null)
        {
            Destroy(jointToBreak);
        }

        if (destroyRopeOnCut)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
