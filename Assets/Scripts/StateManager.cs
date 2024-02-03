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

    // LOCATION PROGRESSION
    // TODO: Location Unlocking
}
