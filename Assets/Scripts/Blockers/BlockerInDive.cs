using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BlockerInDive : MonoBehaviour
{
    public Blocker Blocker;

    [Header("Behavior")]
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;

    [Header("Pathfinding")]
    [SerializeField] private Transform target;

    // Display
    private SpriteRenderer spriteRenderer;
    private Vector3 originalLocalScale;
    private Vector3 flippedLocalScale;

    // Movement
    private Collider2D targetArea;
    private Vector2 randomPosition;
    private AIPath aiPath;

    // Flags
    public bool WasCaptured {get; set;}

    void Start()
    {
        // Display
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();

        if (!Blocker.Stationary)
        {
            // Flip setup
            originalLocalScale = transform.localScale;
            flippedLocalScale = Vector3.Scale(originalLocalScale, new Vector3(-1f, 1f, 1f));

            // Movement setup
            aiPath = GetComponent<AIPath>();
            targetArea = transform.parent.GetComponentInParent<Collider2D>();
            aiPath.maxSpeed = Blocker.Speed;

            // Movement functions
            StartCoroutine(Move());
        }

        // Flags
        WasCaptured = false;
    }

    void Update()
    {
        if(!Blocker.Stationary)
        {
            Flip();
        }
    }

    private void SetSprite()
    {
        if (Blocker.Sprites.Length > 1)
        {
            int randomIndex = Random.Range(0, Blocker.Sprites.Length);
            spriteRenderer.sprite = Blocker.Sprites[randomIndex];
        }

        else
        {
            spriteRenderer.sprite = Blocker.Sprites[0];
        }
    }

    // Patrol within map
    IEnumerator Move()
    {
        while (true)
        {
            randomPosition = SpawnManager.instance.GetRandomMapPosition(targetArea);

            while(Vector2.Distance(transform.position, randomPosition) > 0.2f)
            {
                target.position = randomPosition;

                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }

    // Flip sprite according to direction
    private void Flip()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = flippedLocalScale;
        }

        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = originalLocalScale;
        }
    }
}
