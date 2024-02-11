using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class HallManager : MonoBehaviour
{
    [Header("Creatures")]
    [SerializeField] private Creature[] coralReefCreatures;
    [SerializeField] private Creature[] seagrassBedCreatures;
    [SerializeField] private Creature[] openOceanCreatures;
    
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
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private GameObject inventoryPhotoPrefab;

    [Header("Progress Tracker")]
    [SerializeField] private ProgressTracker progressTracker;

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
        // Sort creature arrays
        SortCreaturesPerBiome();

        // Set + populate
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

    // Sort arrays by conservation status
    private void SortCreaturesPerBiome()
    {
        coralReefCreatures = coralReefCreatures.OrderBy(c => (int)(c.ConservationStatus)).ToArray();
        seagrassBedCreatures = seagrassBedCreatures.OrderBy(c => (int)(c.ConservationStatus)).ToArray();
        openOceanCreatures = openOceanCreatures.OrderBy(c => (int)(c.ConservationStatus)).ToArray();
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
        int numOfGrids = (int)Math.Ceiling((double)(hallCreatures.Length / 10f));
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
            if (hallCreatures[i].CaptureStatus == CreatureStatus.Captured)
            {
                SetUpInventoryItem(hallCreatures[i]);
            }
        }

        // Inventory Manager (for getting current display)
        inventoryManager.DisplayCreatures = hallCreatures;

        // Progress Tracker
        int identifiedCount = hallCreatures.Where(c => c.CaptureStatus == CreatureStatus.Identified).Count();
        progressTracker.Initialize(identifiedCount, hallCreatures.Length);
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
            // Show/hide
            if (creature.CaptureStatus == CreatureStatus.Identified)
            {
                // Change image
                photoItem.SetCreatureImage(creature.Sprite);

                // Change name
                photoItem.SetLabel(creature.CommonName);
            }

            else
            {
                photoItem.SetLabel("???");
            }
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
            item.Creature = creature;
            item.SetDisplay();
        }
    }

    // Set inventory item
    private void SetUpInventoryItem(Creature creature)
    {
        // Instantiate GO
        GameObject newInventoryPhoto = Instantiate(inventoryPhotoPrefab,
                                                   inventoryContent);
        
        // Change image
        if (newInventoryPhoto.TryGetComponent<PhotoItem>(out PhotoItem photoItem))
        {
            photoItem.SetCreatureImage(creature.Sprite);
        }

        // Add to manager
        inventoryManager.InventoryCreatures.Add(creature);

        // Add to item
        if (newInventoryPhoto.TryGetComponent<InventoryItem>(out InventoryItem inventoryItem))
        {
            inventoryItem.Creature = creature;
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
