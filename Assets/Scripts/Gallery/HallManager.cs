using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HallManager : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private Sprite CoralReefBG;
    [SerializeField] private Sprite SeagrassBedBG;
    [SerializeField] private Sprite OpenOceanBG;

    [Header("UI")]
    [SerializeField] private Image BG;

    // STATE
    private Biome currentHall;

    void Start()
    {
        // Current Hall
        currentHall = UniversalManagers.instance.GetComponentInChildren<StateManager>().CurrentHall;

        // Set Up
        SetUpBG();
    }

    private void SetUpBG()
    {
        if (currentHall == Biome.CoralReef)
        {
            BG.sprite = CoralReefBG;
        }

        else if (currentHall == Biome.SeagrassBed)
        {
            BG.sprite = SeagrassBedBG;
        }

        else if (currentHall == Biome.OpenOcean)
        {
            BG.sprite = OpenOceanBG;
        }
    }
}
