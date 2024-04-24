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
    // Transition
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;

    // State Manager
    private StateManager stateManager;

    // Audio Manager
    private AudioManager audioManager;

    void Start()
    {
        // State Manager
        stateManager = UniversalManagers.instance.GetComponentInChildren<StateManager>();

        // Audio Manager
        audioManager = UniversalManagers.instance.GetComponentInChildren<AudioManager>();
    }

    // Basic open scene according to name
    public void OpenScene(string sceneName)
    {
        // MUSIC
        if (SceneManager.GetActiveScene().name == "Biome Hall" && sceneName == "Main Hall")
        {
            // Don't change music
        }

        else
        {
            audioManager.PlaySceneAudio(sceneName);
        }

        // Transition
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Splash SFX
        audioManager.PlaySFX("Splash");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        // Load
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
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

        // Transition
        StartCoroutine(Transition("Biome Hall"));
    }
}
