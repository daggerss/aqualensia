using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HallManager : MonoBehaviour
{
    [Header("Creatures")]
    [SerializeField] private Creature[] coralReefCreatures;
    [SerializeField] private Creature[] seagrassBedCreatures;
    [SerializeField] private Creature[] openOceanCreatures;

    // ! TO DELETE
    [SerializeField] private Creature[] sampleCreatures;
    
    [Header("Assets")]
    [SerializeField] private Sprite coralReefBG;
    [SerializeField] private Sprite seagrassBedBG;
    [SerializeField] private Sprite openOceanBG;

    [Header("Base UI")]
    [SerializeField] private Image BG;
    [SerializeField] private TMP_Text[] plaqueTexts;

    [Header("TOC")]
    [SerializeField] private GameObject tocObject;
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject tocGridPrefab;
    [SerializeField] private GameObject tocItemPrefab;
    [SerializeField] private SwipeController tocSwipe;

    [Header("Creature Display")]
    [SerializeField] private GameObject displayObject;
    [SerializeField] private Transform displayContent;
    [SerializeField] private GameObject displayPrefab;
    [SerializeField] private SwipeController displaySwipe;

    [Header("Inventory")]
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private GameObject inventoryPhotoPrefab;

    // STATE
    private Biome currentHall;

    void Awake()
    {
        // Current Hall
        currentHall = UniversalManagers.instance.GetComponentInChildren<StateManager>().CurrentHall;

        // Set Up
        SetUpAll();
    }

    /* -------------------------------- Setup ------------------------------- */

    // Set up according to biome
    private void SetUpAll()
    {
        // ! TO DELETE
        // SetUpContent(sampleCreatures);

        if (currentHall == Biome.CoralReef)
        {
            SetUpUI(coralReefBG, "Coral Reefs");
            SetUpContent(coralReefCreatures);
        }

        else if (currentHall == Biome.SeagrassBed)
        {
            SetUpUI(seagrassBedBG, "Seagrass Meadows");
            SetUpContent(seagrassBedCreatures);
        }

        else if (currentHall == Biome.OpenOcean)
        {
            SetUpUI(openOceanBG, "Open Ocean");
            SetUpContent(openOceanCreatures);
        }
    }

    // Set BG, hall plaques
    private void SetUpUI(Sprite bgSprite, string hallPlaqueText)
    {
        BG.sprite = bgSprite;

        foreach (TMP_Text plaqueText in plaqueTexts)
        {
            plaqueText.text = hallPlaqueText;
        }
    }

    // Populate TOC, display item, inventory
    private void SetUpContent(Creature[] hallCreatures)
    {
        // TOC Grids Setup
        int numOfGrids = (int)Math.Ceiling((double)(hallCreatures.Length / 10));
        List<GameObject> grids = new List<GameObject>();
        
        for (int i = 0; i < numOfGrids; i++)
        {
            grids.Add(Instantiate(tocGridPrefab, gridParent));
        }

        // Creature Population
        for(int i = 0; i < hallCreatures.Length; i++)
        {
            // TOC
            SetUpTOCItem(hallCreatures[i], grids[i / 10].transform, i);

            // Display
            SetUpDisplayItem(hallCreatures[i]);

            // Inventory
            if (hallCreatures[i].CaptureCount > 0)
            {
                SetUpInventoryItem(hallCreatures[i]);
            }
        }
    }

    // Set TOC item
    private void SetUpTOCItem(Creature creature, Transform grid, int index)
    {
        // Instantiate GO
        GameObject newTOCItem = Instantiate(tocItemPrefab, grid);

        // Get PhotoItem component
        PhotoItem photoItem = newTOCItem.GetComponentInChildren<PhotoItem>();
        
        if (photoItem != null)
        {
            // Change image
            photoItem.SetCreatureImage(creature.Sprite);

            // Change name
            photoItem.SetLabel(creature.CommonName);
        }

        // Get TOCItem component
        if (newTOCItem.TryGetComponent<TOCItem>(out TOCItem tocItem))
        {
            tocItem.SetUp(this, index);
        }
    }

    // Set display item
    private void SetUpDisplayItem(Creature creature)
    {
        // Instantiate GO
        GameObject newDisplayItem = Instantiate(displayPrefab, displayContent);
        
        // Set info
        if (newDisplayItem.TryGetComponent<DisplayItem>(out DisplayItem item))
        {
            item.SetSpriteImage(creature.Sprite);
            item.SetIRLImage(creature.RealPhoto);
            item.SetCommonName(creature.CommonName);
            item.SetScientificName(creature.ScientificName);
            item.SetCategory(creature.ConservationStatus);
            item.SetOceanZone(creature.UpperZone, creature.LowerZone);
            item.SetActiveTime(creature.ActiveTime);
            item.SetCredit(creature.PhotoCredit);
            item.SetResearchInfo(creature.GalleryInfo);
        }
    }

    // Set inventory item
    private void SetUpInventoryItem(Creature creature)
    {
        // Instantiate GO
        GameObject newInventoryPhoto = Instantiate(inventoryPhotoPrefab,
                                                   inventoryContent);
        
        // Change image
        if (newInventoryPhoto.TryGetComponent<PhotoItem>(out PhotoItem item))
        {
            item.SetCreatureImage(creature.Sprite);
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
