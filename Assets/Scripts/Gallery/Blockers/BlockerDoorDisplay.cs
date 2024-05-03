using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockerDoorDisplay : MonoBehaviour
{
    [SerializeField] private Button hallButton;

    void Start()
    {
        // If any blockers captured
        if (UniversalManagers.instance
                             .GetComponentInChildren<BlockerDatabase>()
                             .HasCaptured())
        {
            hallButton.interactable = true;
        }
    }
}
