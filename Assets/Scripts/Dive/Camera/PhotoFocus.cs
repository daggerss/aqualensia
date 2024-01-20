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
    [SerializeField] private TMP_Text creatureNameText;
    [SerializeField] private TMP_Text captureCountText;

    private CreatureInDive creatureInstance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        creatureInstance = other.gameObject.GetComponent<CreatureInDive>();

        SetInfo(creatureInstance.Creature);
        DisplayInfo(true);
        DisplayCapturedLabel(creatureInstance.WasCaptured);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DisplayInfo(false);
        DisplayCapturedLabel(false);
    }

    private void SetInfo(Creature creature)
    {
        // Name
        if (creature.CaptureStatus == CreatureStatus.Identified)
        {
            statusImage.sprite = statusSprites[0];
            statusImage.color = statusColors[0];
            creatureNameText.text = creature.CommonName;
        }

        else if (creature.CaptureStatus == CreatureStatus.Captured)
        {
            statusImage.sprite = statusSprites[1];
            statusImage.color = statusColors[1];
            creatureNameText.text = "???";
        }

        else
        {
            statusImage.sprite = statusSprites[2];
            statusImage.color = statusColors[2];
            creatureNameText.text = "???";
        }

        // Count
        captureCountText.text = creature.CaptureCount + "/4";
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
