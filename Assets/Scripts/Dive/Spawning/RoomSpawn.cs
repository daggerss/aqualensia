using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    [Header("Creatures")]
    [SerializeField] private Creature[] mobileCreatures;
    [SerializeField] private Creature[] sessileCreatures;

    [Header("Blockers")]
    [SerializeField] private Blocker[] mobileBlockers;
    [SerializeField] private Blocker[] stationaryBlockers;

    private Collider2D spawnableArea;

    void Awake()
    {
        spawnableArea = GetComponent<Collider2D>();
    }

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        // Creatures
        SpawnManager.instance.SpawnCreatures(mobileCreatures, transform,
                                             spawnableArea);
        SpawnManager.instance.SpawnCreatures(sessileCreatures, transform);

        // Blockers
        SpawnManager.instance.SpawnBlockers(mobileBlockers, transform,
                                            spawnableArea);
        SpawnManager.instance.SpawnBlockers(stationaryBlockers, transform);
    }
}
