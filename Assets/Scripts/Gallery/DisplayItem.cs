using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class DisplayItem : MonoBehaviour, IDropHandler
{
    public Creature Creature {get; set;}

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

    private IdentificationManager idManager;

    /* --------------------------- Identification --------------------------- */
    void Start()
    {
        idManager = GetComponentInParent<IdentificationManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Creature.CaptureStatus != CreatureStatus.Identified)
        {
            Creature droppedCreature = eventData.pointerDrag.GetComponent<InventoryItem>().Creature;

            if (droppedCreature.CaptureCount == 4)
            {
                bool match = (Creature.ScientificName == droppedCreature.ScientificName);
    
                // Toast
                StartCoroutine(idManager.ShowResult(match));

                if (match)
                {
                    // Set identified
                    Creature.CaptureStatus = CreatureStatus.Identified;
                    
                    // Reveal on display
                    SetIdentified();

                    // Destroy inventory item
                    idManager.DestroyInInventory(eventData.pointerDrag, Creature);
                }
            }
        }
    }

    /* ----------------------------- Display UI ----------------------------- */
    public void SetDisplay()
    {
        // Revealed
        if (Creature.CaptureStatus == CreatureStatus.Identified)
        {
            SetIdentified();
        }

        // Hidden
        else
        {
            SetUnknown();
        }
    }
    
    private void SetUnknown()
    {
        if (Creature != null)
        {
            SetCommonName("???");
            SetScientificName("???");
            SetCategory(Creature.ConservationStatus);
            SetOceanZone(Creature.UpperZone, Creature.LowerZone);
            SetActiveTime(Creature.ActiveTime);
            SetCredit("???");
            SetResearchInfo(Creature.GalleryInfo);
        }
    }
    
    private void SetIdentified()
    {
        if (Creature != null)
        {
            SetSpriteImage(Creature.Sprite);
            SetIRLImage(Creature.RealPhoto);
            SetCommonName(Creature.CommonName);
            SetScientificName(Creature.ScientificName);
            SetCategory(Creature.ConservationStatus);
            SetOceanZone(Creature.UpperZone, Creature.LowerZone);
            SetActiveTime(Creature.ActiveTime);
            SetCredit(Creature.PhotoCredit);
            SetResearchInfo(Creature.GalleryInfo);
        }
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
