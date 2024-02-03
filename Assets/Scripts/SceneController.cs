using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ! Note to Dagny
// Because I know your ass is gonna question this in the name of efficiency
// This is NOT under Universal Managers because I don't want the headache of
// finding its reference for Button OnClick() purposes

public class SceneController : MonoBehaviour
{
    // State Manager
    private StateManager stateManager;

    void Start()
    {
        // State Manager
        stateManager = UniversalManagers.instance.GetComponentInChildren<StateManager>();
    }

    // Basic open scene according to name
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Open biome hall
        // ! Param Note
        // OnClick() doesn't support dropdowns apparently
        // O - Coral Reef
        // 1 - Seagrass Bed
        // 2 - Open Ocean
    public void OpenBiomeHall(int hallBiome)
    {
        // Set current hall
        stateManager.CurrentHall = (Biome)hallBiome;

        // Open scene
        SceneManager.LoadScene("Biome Hall");
    }
}
