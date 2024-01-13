using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConservationStatusUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text text;

    [Header("Colors")]
    [SerializeField] private Color darkTextColor;
    [SerializeField] private Color LCColor;
    [SerializeField] private Color NTColor;
    [SerializeField] private Color VUColor;
    [SerializeField] private Color ENColor;
    [SerializeField] private Color CRColor;
    [SerializeField] private Color NEColor;
    [SerializeField] private Color DDColor;

    public void SetStatus(ConservationStatus status)
    {
        text.color = darkTextColor;

        if (status == ConservationStatus.LC)
        {
            background.color = LCColor;
            text.text = "Least Concern";
        }

        else if (status == ConservationStatus.NT)
        {
            background.color = NTColor;
            text.text = "Near Threatened";
        }

        else if (status == ConservationStatus.VU)
        {
            background.color = VUColor;
            text.text = "Vulnerable";
        }

        else if (status == ConservationStatus.EN)
        {
            background.color = ENColor;
            text.color = Color.white;
            text.text = "Endangered";
        }

        else if (status == ConservationStatus.CR)
        {
            background.color = CRColor;
            text.color = Color.white;
            text.text = "Critically Endangered";
        }

        else if (status == ConservationStatus.NE)
        {
            background.color = NEColor;
            text.text = "Not Evaluated";
        }

        else if (status == ConservationStatus.DD)
        {
            background.color = DDColor;
            text.text = "Data Deficient";
        }
    }
}
