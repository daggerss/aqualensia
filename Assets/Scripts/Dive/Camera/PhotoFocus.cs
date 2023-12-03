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

    [Header("Text")]
    [SerializeField] private TMP_Text creatureNameText;
    [SerializeField] private TMP_Text captureCountText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SetInfo(other.gameObject.GetComponent<CreatureInDive>().Creature);
        DisplayInfo(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        SetInfo(other.gameObject.GetComponent<CreatureInDive>().Creature);
        DisplayInfo(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DisplayInfo(false);
    }

    private void SetInfo(Creature creature)
    {
        // Name
        if (creature.Status == CreatureStatus.Identified)
        {
            statusImage.sprite = statusSprites[0];
            statusImage.color = statusColors[0];
            creatureNameText.text = creature.CommonName;
        }

        else if (creature.Status == CreatureStatus.Captured)
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

    private void DisplayInfo(bool toDisplay)
    {
        foreach (GameObject ui in focusUI)
        {
            ui.SetActive(toDisplay);
        }
    }
}
