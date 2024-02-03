using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HallManager : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private Sprite CoralReefBG;
    [SerializeField] private Sprite SeagrassBedBG;
    [SerializeField] private Sprite OpenOceanBG;

    [Header("UI")]
    [SerializeField] private Image BG;
    [SerializeField] private TMP_Text[] plaqueTexts;

    // STATE
    private Biome currentHall;

    void Start()
    {
        // Current Hall
        currentHall = UniversalManagers.instance.GetComponentInChildren<StateManager>().CurrentHall;

        // Set Up
        SetUpUI();
    }

    private void SetUpUI()
    {
        if (currentHall == Biome.CoralReef)
        {
            BG.sprite = CoralReefBG;

            foreach (TMP_Text plaqueText in plaqueTexts)
            {
                plaqueText.text = "Coral Reefs";
            }
        }

        else if (currentHall == Biome.SeagrassBed)
        {
            BG.sprite = SeagrassBedBG;
            
            foreach (TMP_Text plaqueText in plaqueTexts)
            {
                plaqueText.text = "Seagrass Meadows";
            }
        }

        else if (currentHall == Biome.OpenOcean)
        {
            BG.sprite = OpenOceanBG;
            
            foreach (TMP_Text plaqueText in plaqueTexts)
            {
                plaqueText.text = "Open Ocean";
            }
        }
    }
}
