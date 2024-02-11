using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropZone : MonoBehaviour
{
    [Header("Default")]
    [SerializeField] private Color defaultBGColor;
    [SerializeField] private Color defaultForeColor;
    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private string defaultLabel;

    [Header("Within Range")]
    [SerializeField] private Color withinRangeBGColor;
    [SerializeField] private Color withinRangeForeColor;
    [SerializeField] private Sprite withinRangeIcon;
    [SerializeField] private string withinRangeLabel;

    [Header("Error")]
    [SerializeField] private Color errorBGColor;
    [SerializeField] private Color errorForeColor;
    [SerializeField] private Sprite errorIcon;
    [SerializeField] private string errorLabel;

    [Header("UI")]
    [SerializeField] private Image baseImage;
    [SerializeField] private Image borderImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image labelBG;
    [SerializeField] private TMP_Text labelText;

    private Color labelColor;

    public void Toggle(bool visible, ZoneState state)
    {
        // Show
        if (visible)
        {
            if (state == ZoneState.Default)
            {
                SetDefault();
            }
    
            else if (state == ZoneState.WithinRange)
            {
                SetWithinRange();
            }
    
            else if (state == ZoneState.Error)
            {
                SetError();
            }
    
            gameObject.SetActive(true);
        }

        // Hide
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void SetDefault()
    {
        // Base
        baseImage.color = defaultBGColor;
        borderImage.color = defaultForeColor;

        // Icon
        iconImage.color = defaultForeColor;
        iconImage.sprite = defaultIcon;

        // Label
        labelColor = defaultForeColor;
        labelColor.a = 0.8f;
        labelBG.color = labelColor;
        labelText.text = defaultLabel;
    }

    private void SetWithinRange()
    {
        // Base
        baseImage.color = withinRangeBGColor;
        borderImage.color = withinRangeForeColor;

        // Icon
        iconImage.color = withinRangeForeColor;
        iconImage.sprite = withinRangeIcon;

        // Label
        labelColor = withinRangeForeColor;
        labelColor.a = 0.8f;
        labelBG.color = labelColor;
        labelText.text = withinRangeLabel;
    }

    private void SetError()
    {
        // Base
        baseImage.color = errorBGColor;
        borderImage.color = errorForeColor;

        // Icon
        iconImage.color = errorForeColor;
        iconImage.sprite = errorIcon;

        // Label
        labelColor = errorForeColor;
        labelColor.a = 0.8f;
        labelBG.color = labelColor;
        labelText.text = errorLabel;
    }
}
