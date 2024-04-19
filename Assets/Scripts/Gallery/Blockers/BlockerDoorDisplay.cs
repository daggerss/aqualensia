using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerDoorDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] blockerUI;

    void Start()
    {
        // If any blockers captured
        if (UniversalManagers.instance
                             .GetComponentInChildren<BlockerDatabase>()
                             .HasCaptured())
        {
            ShowUI();
        }
    }

    void ShowUI()
    {
        foreach (GameObject ui in blockerUI)
        {
            ui.SetActive(true);
        }
    }
}
