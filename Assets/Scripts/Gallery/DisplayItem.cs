using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DisplayItem : MonoBehaviour
{
    [Header("Frames")]
    [SerializeField] private PhotoItem spriteFrame;
    [SerializeField] private PhotoItem irlFrame;

    [Header("Name Plaque")]
    [SerializeField] private TMP_Text commonNameText;
    [SerializeField] private TMP_Text scientificNameText;

    [Header("Detail Plaques")]
    [SerializeField] private ConservationStatusUI categoryUI;
    [SerializeField] private TMP_Text zoneText;
    [SerializeField] private TMP_Text creditText;

    [Header("Active Time")]
    [SerializeField] private TMP_Text activeTimeText;
    [SerializeField] private Image sunIcon;
    [SerializeField] private Image moonIcon;

    [Header("Info Plaque")]
    [SerializeField] private TMP_Text infoText;


    public void SetSpriteImage(Sprite img)
    {
        spriteFrame.SetCreatureImage(img);
    }

    public void SetIRLImage(Sprite img)
    {
        irlFrame.SetCreatureImage(img);
    }

    public void SetCommonName(string txt)
    {
        commonNameText.text = txt;
    }

    public void SetScientificName(string txt)
    {
        scientificNameText.text = txt;
    }

    public void SetCategory(ConservationStatus status)
    {
        categoryUI.SetStatus(status);
    }

    public void SetOceanZone(OceanZone upper, OceanZone lower)
    {
        // 1 Zone only
        if (upper == lower)
        {
            zoneText.text = upper.ToString();
        }

        // 2+ Zones
        else
        {
            zoneText.text = String.Format("{0} to {1}", upper.ToString(), lower.ToString());
        }
    }

    public void SetActiveTime(TimeOfDay time)
    {
        activeTimeText.text = time.ToString();

        if (time == TimeOfDay.Day)
        {
            moonIcon.gameObject.SetActive(false);
        }

        else if (time == TimeOfDay.Night)
        {
            sunIcon.gameObject.SetActive(false);
        }
    }

    public void SetCredit(string txt)
    {
        creditText.text = txt;
    }

    public void SetResearchInfo(string txt)
    {
        infoText.text = String.Format("<line-indent=5%>{0}", txt);
    }
}
