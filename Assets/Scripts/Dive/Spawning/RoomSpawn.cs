using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    [SerializeField] private Creature[] mobileCreatures;
    [SerializeField] private Creature[] sessileCreatures;

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
        SpawnManager.instance.SpawnCreatures(mobileCreatures, spawnableArea);
        SpawnManager.instance.SpawnCreatures(sessileCreatures);
    }
}
