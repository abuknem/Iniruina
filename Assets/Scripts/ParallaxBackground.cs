using UnityEngine;
using static UnityEngine.Audio.GeneratorInstance;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Target (Player or Camera)")]
    public Transform trackingTarget;

    private Transform[] backgrounds;
    private float[] startPositionsX;
    private float spriteWidth;

    [Range(0f, 2f)]
    public float parallaxSpeed = 0.5f;

    void Start()
    {
        if (trackingTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) trackingTarget = player.transform;
            else if (Camera.main != null) trackingTarget = Camera.main.transform;
        }

        int count = transform.childCount;
        backgrounds = new Transform[count];
        startPositionsX = new float[count];

        for (int i = 0; i < count; i++)
        {
            backgrounds[i] = transform.GetChild(i);
            startPositionsX[i] = backgrounds[i].position.x;
        }

        if (count > 0)
        {
            SpriteRenderer sr = backgrounds[0].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                spriteWidth = sr.bounds.size.x;
            }
            else
            {
                spriteWidth = 20f;
            }
        }
    }

    void LateUpdate()
    {
        if (trackingTarget == null) return;

        Vector3 parentPos = transform.position;
        parentPos.x = trackingTarget.position.x;
        transform.position = parentPos;

        float targetX = trackingTarget.position.x;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speedMultiplier = (float)(i + 1) / backgrounds.Length * parallaxSpeed;

            float distanceMoved = targetX * (1 - speedMultiplier);

            float newX = startPositionsX[i] + ((targetX - startPositionsX[i]) * speedMultiplier);

            float offsetFromTarget = targetX - backgrounds[i].position.x;

            if (offsetFromTarget > spriteWidth)
            {
                startPositionsX[i] += spriteWidth;
            }
            else if (offsetFromTarget < -spriteWidth)
            {
                startPositionsX[i] -= spriteWidth;
            }

            Vector3 pos = backgrounds[i].position;
            pos.x = startPositionsX[i] + ((targetX - startPositionsX[i]) * speedMultiplier);
            backgrounds[i].position = pos;
        }
    }
}