using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text paragraphText;
    [SerializeField] TMP_Text[] emphasisText;
    [SerializeField] TMP_Text costText;
    [SerializeField] Button primaryBtn;

    [Header("Colors")]
    [SerializeField] Color errorColor;

    public void SetParagraph(string txt)
    {
        paragraphText.text = txt;
    }

    public void SetEmphasis(string[] txt)
    {
        // Check size match
        if (txt.Length <= emphasisText.Length)
        {
            for (int i = 0; i < txt.Length; i++)
            {
                emphasisText[i].text = txt[i];
            }
        }

        else
        {
            Debug.LogError("[DIALOGUE] Not enough emphasis text UI");
        }
    }

    public void SetCost(int num, bool insufficient = false)
    {
        costText.text = num.ToString("N0");

        // Insufficient funds
        if (insufficient)
        {
            // Change cost color
            costText.color = errorColor;
            
            // Disable fund button
            primaryBtn.interactable = false;
        }

        // Sufficient funds
        else
        {
            // Enable fund button
            primaryBtn.interactable = true;
        }
    }
}
