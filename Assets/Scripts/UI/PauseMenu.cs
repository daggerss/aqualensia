using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject exitDiveBtn;

    private StateManager stateManager;
    private StatsManager diveStatsManager;

    void Start()
    {
        // Get state manager
        stateManager = UniversalManagers.instance.GetComponentInChildren<StateManager>();

        if (stateManager.InDive())
        {
            // Get stats manager
            diveStatsManager = FindObjectOfType<StatsManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (SceneManager.GetActiveScene().name != "Start")
            {
                // Pause
                Pause();
            }
        }
    }

    // Pause
    private void Pause()
    {
        // Stop time
        Time.timeScale = 0;

        // Show all UI
        pauseMenuUI.SetActive(true);

        // Check if in dive
        if (stateManager.InDive())
        {
            // Show exit dive
            exitDiveBtn.SetActive(true);
        }

        else
        {
            // Hide exit dive
            exitDiveBtn.SetActive(false);
        }
    }

    // Resume
    public void Resume()
    {
        // Start time
        Time.timeScale = 1;

        // Hide all UI
        pauseMenuUI.SetActive(false);
    }

    // Exit Dive
    public void ExitDive()
    {
        if (diveStatsManager != null)
        {
            // Ascend
            StartCoroutine(diveStatsManager.Ascend());
        }
    }
}
