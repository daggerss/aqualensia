using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // Current Location
        string currentLocation = SceneManager.GetActiveScene().name;

        // Creatures
        SpawnCreatures();

        // Blockers (if location blocked & tutorial is ready)
        if (UniversalManagers.instance.GetComponentInChildren<StateManager>()
                                      .LocationBlockStates[currentLocation] &&
            PlayerPrefs.GetInt("TutorialSequence", 0) >= 6)
        {
            SpawnBlockers();
        }
    }

    // Spawn mobile and sessile creatures
    private void SpawnCreatures()
    {
        SpawnManager.instance.SpawnCreatures(mobileCreatures, transform,
                                             spawnableArea);
        SpawnManager.instance.SpawnCreatures(sessileCreatures, transform);
    }

    // Spawn mobile and stationary blockers
    private void SpawnBlockers()
    {
        SpawnManager.instance.SpawnBlockers(mobileBlockers, transform,
                                            spawnableArea);
        SpawnManager.instance.SpawnBlockers(stationaryBlockers, transform);
    }
}
