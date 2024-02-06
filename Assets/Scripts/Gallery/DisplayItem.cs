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


    public void SetUnknown(Creature creature)
    {
        SetCommonName("???");
        SetScientificName("???");
        SetCategory(creature.ConservationStatus);
        SetOceanZone(creature.UpperZone, creature.LowerZone);
        SetActiveTime(creature.ActiveTime);
        SetCredit("???");
        SetResearchInfo(creature.GalleryInfo);
    }
    
    public void SetIdentified(Creature creature)
    {
        SetSpriteImage(creature.Sprite);
        SetIRLImage(creature.RealPhoto);
        SetCommonName(creature.CommonName);
        SetScientificName(creature.ScientificName);
        SetCategory(creature.ConservationStatus);
        SetOceanZone(creature.UpperZone, creature.LowerZone);
        SetActiveTime(creature.ActiveTime);
        SetCredit(creature.PhotoCredit);
        SetResearchInfo(creature.GalleryInfo);
    }

    private void SetSpriteImage(Sprite img)
    {
        spriteFrame.SetCreatureImage(img);
    }

    private void SetIRLImage(Sprite img)
    {
        irlFrame.SetCreatureImage(img);
    }

    private void SetCommonName(string txt)
    {
        commonNameText.text = txt;
    }

    private void SetScientificName(string txt)
    {
        scientificNameText.text = txt;
    }

    private void SetCategory(ConservationStatus status)
    {
        categoryUI.SetStatus(status);
    }

    private void SetOceanZone(OceanZone upper, OceanZone lower)
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

    private void SetActiveTime(TimeOfDay time)
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

    private void SetCredit(string txt)
    {
        creditText.text = txt;
    }

    private void SetResearchInfo(string txt)
    {
        infoText.text = String.Format("<line-indent=5%>{0}", txt);
    }
}
