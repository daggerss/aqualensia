using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class BlockerDisplayItem : MonoBehaviour
{
    public ParentBlocker Blocker {get; set;}

    [Header("Frames")]
    [SerializeField] private PhotoItem spriteFrame;

    [Header("Name Plaque")]
    [SerializeField] private TMP_Text nameText;

    [Header("Detail Plaques")]
    [SerializeField] private BlockerTypeUI categoryUI;
    [SerializeField] private TMP_Text locationText;
    [SerializeField] private TMP_Text captureCountText;

    [Header("Info Plaque")]
    [SerializeField] private TMP_Text infoText;

    private CurrencyManager currencyManager;

    /* --------------------------- Identification --------------------------- */
    void Start()
    {
        // Get managers
        currencyManager = UniversalManagers.instance.GetComponentInChildren<CurrencyManager>();
    }

    /* ----------------------------- Display UI ----------------------------- */
    public void SetDisplay()
    {
        if (Blocker != null)
        {
            SetSpriteImage(Blocker.Sprite);
            SetName(Blocker.Name);
            SetCategory(Blocker.BlockerType);
            SetLocation(Blocker.Location);
            SetCaptureCount(Blocker.CaptureCount);
            SetResearchInfo(Blocker.GalleryInfo);
        }
    }

    private void SetSpriteImage(Sprite img)
    {
        spriteFrame.SetBG(Blocker.Biome);
        spriteFrame.SetItemImage(img);
    }

    private void SetName(string txt)
    {
        nameText.text = txt;
    }

    private void SetCategory(BlockerType type)
    {
        categoryUI.SetType(type);
    }

    private void SetLocation(string txt)
    {
        locationText.text = txt;
    }

    private void SetCaptureCount(int count)
    {
        captureCountText.text = (4 - count).ToString();
    }

    private void SetResearchInfo(string txt)
    {
        infoText.text = String.Format("<line-indent=5%>{0}", txt);
    }
}
