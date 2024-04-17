using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlockerTypeUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text text;

    [Header("Colors")]
    [SerializeField] private Color darkTextColor;
    [SerializeField] private Color pollutionColor;
    [SerializeField] private Color fishingColor;
    [SerializeField] private Color developmentColor;

    public void SetType(BlockerType type)
    {
        text.color = darkTextColor;

        if (type == BlockerType.Pollution)
        {
            background.color = pollutionColor;
            text.text = "Pollution";
        }

        else if (type == BlockerType.Fishing)
        {
            background.color = fishingColor;
            text.color = Color.white;
            text.text = "Destructive Fishing";
        }

        else if (type == BlockerType.Development)
        {
            background.color = developmentColor;
            text.color = Color.white;
            text.text = "Harmful Development";
        }
    }
}
