using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CreatureInDive : MonoBehaviour
{
    public Creature Creature;

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
    public bool wasCaptured {get; set;}

    void Start()
    {
        // Display
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Creature.Sprite;

        if (!Creature.Sessile)
        {
            // Flip setup
            originalLocalScale = transform.localScale;
            flippedLocalScale = Vector3.Scale(originalLocalScale, new Vector3(-1f, 1f, 1f));

            // Movement setup
            aiPath = GetComponent<AIPath>();
            targetArea = transform.parent.GetComponentInParent<Collider2D>();
            aiPath.maxSpeed = Creature.Speed;

            // Movement functions
            StartCoroutine(Move());
        }

        // Flags
        wasCaptured = false;
    }

    void Update()
    {
        if(!Creature.Sessile)
        {
            Flip();
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
