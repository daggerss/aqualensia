using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] private GameObject creaturePrefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private LayerMask layersNotToSpawnOn;
    [SerializeField] private float creatureRadius;

    private CreatureInDive creatureInfo;

    void Awake()
    {
        // Make singleton
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SpawnCreatures(Collider2D spawnableAreaCollider, Creature[] creatures)
    {
        creatureInfo = creaturePrefab.GetComponent<CreatureInDive>();

        foreach (Creature creature in creatures)
        {
            // TODO: spawn based on population scale
            creatureInfo.creature = creature;

            Vector2 spawnPosition = GetRandomSpawnPosition(spawnableAreaCollider);

            // Spawn only if valid
            if (!spawnPosition.Equals(Vector2.zero))
            {
                GameObject spawnedCreature = Instantiate(creaturePrefab, spawnPosition, Quaternion.identity);

                // Organize in hierarchy
                if (parentTransform != null)
                {
                    spawnedCreature.transform.SetParent(parentTransform);
                }
            }
        }
    }

    private Vector2 GetRandomSpawnPosition(Collider2D spawnableAreaCollider)
    {
        // Set up
        Vector2 spawnPosition = Vector2.zero;
        
        // Look for valid spawn position at maximum of 200 attempts
        for (int i = 0; i < 200; i++)
        {
            spawnPosition = GetRandomPointInCollider(spawnableAreaCollider);
            
            if (!isSpawnOverlapping(spawnPosition))
            {
                return spawnPosition;
            }
        }

        // Warning if none found
        Debug.LogWarning("Could not find a valid spawn position");
        return Vector2.zero;
    }

    private Vector2 GetRandomPointInCollider(Collider2D collider, float offset = 1f)
    {
        // Set up offset
        Bounds colliderBounds = collider.bounds;
        Vector2 minBounds = new Vector2(colliderBounds.min.x + offset, colliderBounds.min.y + offset);
        Vector2 maxBounds = new Vector2(colliderBounds.max.x - offset, colliderBounds.max.y - offset);

        // Get random values
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomX, randomY);
    }

    private bool isSpawnOverlapping(Vector2 spawnPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, creatureRadius);

        // Check if on invalid layers
        foreach (Collider2D collider in colliders)
        {
            if (((1 << collider.gameObject.layer) & layersNotToSpawnOn) != 0)
            {
                return true;
            }
        }

        return false;
    }
}
