using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerDoorDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] blockerUI;

    void Start()
    {
        // Get all blockers
        ParentBlocker[] allBlockers = UniversalManagers.instance.GetComponentInChildren<BlockerDatabase>().AllBlockers;
        
        // Check each blocker if captured
        foreach (ParentBlocker blocker in allBlockers)
        {
            if (blocker.CaptureCount > 0)
            {
                ShowUI();
                break;
            }
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
