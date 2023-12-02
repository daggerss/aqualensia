using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    [SerializeField] private Creature[] creatures;

    private Collider2D spawnableArea;

    void Awake()
    {
        spawnableArea = GetComponent<Collider2D>();
    }

    void Start()
    {
        SpawnManager.instance.SpawnCreatures(spawnableArea, creatures);
    }
}
