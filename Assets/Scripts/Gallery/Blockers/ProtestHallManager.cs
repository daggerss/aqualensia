using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class ProtestHallManager : MonoBehaviour
{
    [Header("TOC")]
    [SerializeField] private GameObject tocObject;
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject tocGridPrefab;
    [SerializeField] private GameObject tocItemPrefab;
    [SerializeField] private SwipeController tocSwipe;

    [Header("Blocker Display")]
    [SerializeField] private GameObject displayObject;
    [SerializeField] private Transform displayContent;
    [SerializeField] private GameObject displayPrefab;
    [SerializeField] private SwipeController displaySwipe;

    // BLOCKERS
    private List<ParentBlocker> capturedBlockers = new List<ParentBlocker>();

    // TOC
    private List<GameObject> grids = new List<GameObject>();

    void Awake()
    {
        // Populate
        SetUpContent(UniversalManagers.instance
                                      .GetComponentInChildren<BlockerDatabase>()
                                      .GetAnyCaptured());
    }

    /* -------------------------------- Setup ------------------------------- */
    // Populate TOC, display item
    private void SetUpContent(ParentBlocker[] currentBlockers)
    {
        // TOC Grids Setup
        int numOfGrids = (int)Math.Ceiling((double)(currentBlockers.Length / 10f));
        
        for (int i = 0; i < numOfGrids; i++)
        {
            grids.Add(Instantiate(tocGridPrefab, gridParent));
        }

        // ParentBlocker Population
        for(int i = 0; i < currentBlockers.Length; i++)
        {
            // TOC
            SetUpTOCItem(currentBlockers[i], grids[i / 10].transform, i);

            // Display
            SetUpDisplayItem(currentBlockers[i]);
        }
    }

    // Set TOC item
    private void SetUpTOCItem(ParentBlocker blocker, Transform grid, int index)
    {
        // Instantiate GO
        GameObject newTOCItem = Instantiate(tocItemPrefab, grid);

        // Get PhotoItem component
        PhotoItem photoItem = newTOCItem.GetComponentInChildren<PhotoItem>();
        
        if (photoItem != null)
        {
            // Change BG
            photoItem.SetBG(blocker.Biome);

            // Change image
            photoItem.SetItemImage(blocker.Sprite);

            // Change name
            photoItem.SetLabel(blocker.Name);
        }

        // Get TOCItem component
        if (newTOCItem.TryGetComponent<TOCItem>(out TOCItem tocItem))
        {
            tocItem.SetUp(this, index);
        }
    }

    // Set display item
    private void SetUpDisplayItem(ParentBlocker blocker)
    {
        // Instantiate GO
        GameObject newDisplayItem = Instantiate(displayPrefab, displayContent);
        
        // Set info
        if (newDisplayItem.TryGetComponent<BlockerDisplayItem>(out BlockerDisplayItem item))
        {
            item.Blocker = blocker;
            item.SetDisplay();
        }
    }

    /* ----------------------------- Navigation ----------------------------- */

    // Switch displays and jump
    public void SelectForTOC(int pageNum)
    {
        // Show + jump main display
        displayObject.SetActive(true);
        displaySwipe.Jump(pageNum);

        // Hide + reset TOC
        tocObject.SetActive(false);
        tocSwipe.Jump(1);
    }
}
