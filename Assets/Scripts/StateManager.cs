using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // TIME OF DAY
    [field: SerializeField]
    public TimeOfDay CurrentTimeOfDay {get; set;}

    // GALLERY: HALLS
    [field: SerializeField]
    public Biome CurrentHall {get; set;}

    // LOCATION UNLOCKING
    // TODO: Location Unlocking

    // LOCATION RESTORING
    public Dictionary<string, bool> LocationBlockStates = new Dictionary<string, bool>()
    {
        {"C1", true},
        {"C2", true},
        {"C3", true},
        {"S1", true},
        {"S2", true},
        {"S3", true},
        {"O1", true},
        {"O2", true},
        {"O3", true}
    };
}
