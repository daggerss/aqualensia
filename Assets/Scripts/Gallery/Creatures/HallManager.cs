using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class HallManager : MonoBehaviour
{
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

    // CREATURES
    private Creature[] currentHallCreatures;

    // TOC
    private List<GameObject> grids = new List<GameObject>();

    void Awake()
    {
        // Current Hall
        currentHall = UniversalManagers.instance.GetComponentInChildren<StateManager>().CurrentHall;

        // Current Creatures
        currentHallCreatures = UniversalManagers.instance.GetComponentInChildren<CreatureDatabase>().GetCreatures(currentHall);

        // Set Up
        SetUpAll();
    }

    /* -------------------------------- Setup ------------------------------- */

    // Set up according to biome
    private void SetUpAll()
    {
        // Populate hall
        SetUpCreatures();

        // Set biome UI
        if (currentHall == Biome.CoralReef)
        {
            SetUpUI(coralReefBG, "Coral Reefs");
        }

        else if (currentHall == Biome.SeagrassBed)
        {
            SetUpUI(seagrassBedBG, "Seagrass Meadows");
        }

        else if (currentHall == Biome.OpenOcean)
        {
            SetUpUI(openOceanBG, "Open Ocean");
        }
    }

    private void SetUpCreatures()
    {
        if (currentHallCreatures != null)
        {
            // Sort by conservation status
            currentHallCreatures = currentHallCreatures.OrderBy(c => (int)(c.ConservationStatus)).ToArray();

            // Set up
            SetUpContent(currentHallCreatures);
        }

        else
        {
            Debug.LogWarning("Current hall creatures not found");
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
        int numOfGrids = (int)Math.Ceiling((double)(hallCreatures.Length / 10f));
        
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
                photoItem.SetItemImage(creature.Sprite);
            }

            // Change name
            photoItem.SetLabel(creature.CommonName);
        }

        // Get TOCItem component
        if (newTOCItem.TryGetComponent<TOCItem>(out TOCItem tocItem))
        {
            tocItem.SetUp(this, index);
        }
    }

    // Set TOC item based on current page
    public void RebuildTOCItemOfCurrentPage()
    {
        // Get index
        int index = displaySwipe.CurrentPage - 1;

        // Get photo item from TOC item at index
        PhotoItem photoItem = grids[index / 10].transform
                                               .GetChild(index - (10 * (index / 10)))
                                               .GetComponentInChildren<PhotoItem>();

        // Rebuild TOC photo
        Creature creature = currentHallCreatures[index];

        if (photoItem != null)
        {
            // Show/hide
            if (creature.CaptureStatus == CreatureStatus.Identified)
            {
                // Change image
                photoItem.SetItemImage(creature.Sprite);
            }
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
            photoItem.SetItemImage(creature.Sprite);
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
