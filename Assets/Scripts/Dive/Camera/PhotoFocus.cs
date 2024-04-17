using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoFocus : MonoBehaviour
{
    [SerializeField] private GameObject[] focusUI;

    [Header("Status")]
    [SerializeField] private Image statusImage;
    [SerializeField] private Sprite[] statusSprites;
    [SerializeField] private Color[] statusColors;
    [SerializeField] private GameObject capturedLabel;

    [Header("Text")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text captureCountText;

    private CreatureInDive creatureInstance;
    private BlockerInDive blockerInstance;

    private void OnTriggerStay2D(Collider2D other)
    {
        creatureInstance = other.gameObject.GetComponent<CreatureInDive>();

        if (creatureInstance != null)
        {
            SetCreatureInfo(creatureInstance.Creature);
            DisplayCapturedLabel(creatureInstance.WasCaptured);
        }

        else
        {
            blockerInstance = other.gameObject.GetComponent<BlockerInDive>();
            SetBlockerInfo(blockerInstance.Blocker);
            DisplayCapturedLabel(blockerInstance.WasCaptured);
        }

        DisplayInfo(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DisplayInfo(false);
        DisplayCapturedLabel(false);
    }

    private void SetCreatureInfo(Creature creature)
    {
        // Show
        statusImage.gameObject.SetActive(true);

        // Name
        if (creature.CaptureStatus == CreatureStatus.Identified)
        {
            statusImage.sprite = statusSprites[0];
            statusImage.color = statusColors[0];
            nameText.text = creature.CommonName;
        }

        else if (creature.CaptureStatus == CreatureStatus.Captured)
        {
            statusImage.sprite = statusSprites[1];
            statusImage.color = statusColors[1];
            nameText.text = "???";
        }

        else
        {
            statusImage.sprite = statusSprites[2];
            statusImage.color = statusColors[2];
            nameText.text = "???";
        }

        // Count
        captureCountText.text = creature.CaptureCount + "/4";
    }

    private void SetBlockerInfo(Blocker blocker)
    {
        // Hide
        statusImage.gameObject.SetActive(false);

        // Name
        nameText.text = blocker.Name;

        // Count
        captureCountText.text = blocker.ParentBlocker.CaptureCount + "/4";
    }

    private void DisplayCapturedLabel(bool toDisplay)
    {
        capturedLabel.SetActive(toDisplay);
    }

    private void DisplayInfo(bool toDisplay)
    {
        foreach (GameObject ui in focusUI)
        {
            ui.SetActive(toDisplay);
        }
    }
}
