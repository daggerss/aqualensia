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
    public bool WasCaptured {get; set;}

    void Start()
    {
        // Display
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();

        // Set Radius
        if (Creature.Radius > 0)
        {
            GetComponent<CircleCollider2D>().radius = Creature.Radius;
        }

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
        WasCaptured = false;
    }

    void Update()
    {
        if(!Creature.Sessile)
        {
            Flip();
        }
    }

    void SetSprite()
    {
        if (Creature.SchoolSprite == null)
        {
            spriteRenderer.sprite = Creature.Sprite;
        }

        else
        {
            spriteRenderer.sprite = Creature.SchoolSprite;
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

    // Flee for 1 second
    public IEnumerator Flee()
    {
        // Change target position
        randomPosition = SpawnManager.instance.GetRandomMapPosition(targetArea);

        // Speed up
        aiPath.maxSpeed = 50;

        // Wait 1 second
        yield return new WaitForSeconds(1);

        // Return to default
        aiPath.maxSpeed = Creature.Speed;
    }
}
