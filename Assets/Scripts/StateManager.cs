using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    // TIME OF DAY
    [field: SerializeField]
    public TimeOfDay CurrentTimeOfDay {get; set;}

    // GALLERY: HALLS
    [field: SerializeField]
    public Biome CurrentHall {get; set;}

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

    // Check if currently in dive
    public bool InDive()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        return currentScene == "C1" || currentScene == "C2" || currentScene == "C3" ||
               currentScene == "S1" || currentScene == "S2" || currentScene == "S3" ||
               currentScene == "O1" || currentScene == "O2" || currentScene == "O3";
    }
}
