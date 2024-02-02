using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] private GameObject creaturePrefab;

    [Header("Fixed Points Spawn")]
    [SerializeField] private Vector2[] sessilePositions;

    [Header("Dynamic Spawn")]
    [SerializeField] private LayerMask layersNotToSpawnOn;
    [SerializeField] private float creatureRadius;

    private CreatureInDive creatureInfo;
    private StateManager stateManager;
    private List<int> usedSessilePositions = new List<int>();

    void Awake()
    {
        // Make singleton
        if (instance == null)
        {
            instance = this;
        }

        // Setup
        stateManager = UniversalManagers.instance.GetComponentInChildren<StateManager>();
    }

    // Spawn mobile or sessile
    public void SpawnCreatures(Creature[] creatures, Transform parentTransform,
                               Collider2D spawnableAreaCollider = null)
    {
        // Instantiate variables
        creatureInfo = creaturePrefab.GetComponent<CreatureInDive>();
        bool forSessile = (spawnableAreaCollider == null);
        Vector2 spawnPosition;

        foreach (Creature creature in creatures)
        {
            // Spawn creatures based on blocked and active time
            if (!creature.isBlocked && ((creature.ActiveTime == stateManager.CurrentTimeOfDay) ||
                                        (creature.ActiveTime == TimeOfDay.Both)))
            {
                // Set prefab's creature
                creatureInfo.Creature = creature;
    
                // Spawn based on rarity
                for (int i = 0; i < creature.PopulationScale; i++)
                {
                    if (forSessile) // Sessile position
                    {
                        spawnPosition = GetRandomFixedPosition();
                    }

                    else // Dynamic position
                    {
                        spawnPosition = GetRandomMapPosition(spawnableAreaCollider);
                    }
    
                    // Spawn only if valid
                    if (!spawnPosition.Equals(Vector2.zero))
                    {
                        GameObject spawnedCreature = Instantiate(creaturePrefab,
                                                                 spawnPosition,
                                                                 Quaternion.identity,
                                                                 parentTransform);
                    }
                }
            }
        }
    }

    public Vector2 GetRandomMapPosition(Collider2D spawnableAreaCollider)
    {
        // Set up
        Vector2 mapPosition = Vector2.zero;
        
        // Look for valid spawn position at maximum of 200 attempts
        for (int i = 0; i < 200; i++)
        {
            mapPosition = GetRandomPointInCollider(spawnableAreaCollider);
            
            if (!isPositionOverlapping(mapPosition))
            {
                return mapPosition;
            }
        }

        // Warning if none found
        Debug.LogWarning("Could not find a valid map spawn position");
        return Vector2.zero;
    }

    private Vector2 GetRandomFixedPosition()
    {
        int randomIndex = 0;

        // Look for valid spawn position at maximum of 200 attempts        
        for (int i = 0; i < 200; i++)
        {
            randomIndex = Random.Range(0, sessilePositions.Length);
    
            if (!usedSessilePositions.Contains(randomIndex))
            {
                usedSessilePositions.Add(randomIndex);
                return sessilePositions[randomIndex];
            }
        }

        // Warning if none found
        Debug.LogWarning("Could not find a valid sessile spawn position");
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

    private bool isPositionOverlapping(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, creatureRadius);

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
