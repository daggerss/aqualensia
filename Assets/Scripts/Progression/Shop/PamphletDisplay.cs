using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PamphletDisplay : MonoBehaviour, ISelectHandler
{
    [SerializeField] ShopManager shopManager;
    private StateManager stateManager;

    [SerializeField]
    private ParentBlocker _blocker;
    public ParentBlocker Blocker => _blocker;

    [Header("UI")]
    [SerializeField] private Image blockerImage;
    [SerializeField] private GameObject checkIcon;
    private Image pamphletImage;
    private Button pamphletButton;

    [Header("Coral Reef Sprites")]
    [SerializeField] private Sprite coralReefDefault;
    [SerializeField] private SpriteState coralReefState;

    [Header("Seagrass Bed Sprites")]
    [SerializeField] private Sprite seagrassDefault;
    [SerializeField] private SpriteState seagrassState;

    [Header("Open Ocean Sprites")]
    [SerializeField] private Sprite openOceanDefault;
    [SerializeField] private SpriteState openOceanState;

    private Sprite currentDefault;
    private SpriteState currentSprState;

    void Start()
    {
        // Get managers
        stateManager = UniversalManagers.instance.GetComponentInChildren<StateManager>();

        // Get UI components
        pamphletImage = GetComponent<Image>();
        pamphletButton = GetComponent<Button>();

        // Set display and button assets
        if (_blocker != null)
        {
            // Hide if not fully captured
            if (_blocker.CaptureCount < 4)
            {
                gameObject.SetActive(false);
            }

            else
            {
                SetDisplay();
                SetCompleteState(!stateManager.LocationBlockStates[_blocker.LocationCode]);
            }
        }
    }

    // Set display according to blocker
    private void SetDisplay()
    {
        // Blocker
        blockerImage.sprite = _blocker.Sprite;

        // Set Current
        if (_blocker.Biome == Biome.CoralReef)
        {
            currentDefault = coralReefDefault;
            currentSprState = coralReefState;
        }

        else if (_blocker.Biome == Biome.SeagrassBed)
        {
            currentDefault = seagrassDefault;
            currentSprState = seagrassState;
        }

        else if (_blocker.Biome == Biome.OpenOcean)
        {
            currentDefault = openOceanDefault;
            currentSprState = openOceanState;
        }

        // Set Sprite Swap
        ChangeSpriteSwap(currentDefault, currentSprState);
    }

    // Apply sprite states to button
    private void ChangeSpriteSwap(Sprite normal, SpriteState sprState)
    {
        // Default
        pamphletButton.image.sprite = normal;

        // States
        pamphletButton.spriteState = sprState;
    }

    // Set check + newspaper
    public void SetCompleteState(bool completed)
    {
        // To Fund
        if (!completed)
        {
            pamphletButton.onClick.AddListener(shopManager.ShowFundDialogue);
        }

        else
        {
            // Show
            checkIcon.SetActive(true);

            // Remove previous listeners
            pamphletButton.onClick.RemoveAllListeners();

            // Newspaper
            pamphletButton.onClick.AddListener(shopManager.ShowNewspaper);
        }
    }

    // Do this when the selectable UI object is selected
    public void OnSelect(BaseEventData eventData)
    {
        // Deselect previous pamphlet
        if (shopManager.CurrentPamphlet != null)
        {
            shopManager.CancelFund();
        }

        // Pass self to Shop Manager
        shopManager.CurrentPamphlet = this;

        // Set normal to selected state for funds
        if (stateManager.LocationBlockStates[_blocker.LocationCode])
        {
            pamphletButton.image.sprite = currentSprState.selectedSprite;
        }
    }

    // Reset sprite swap normal
    public void ResetNormalState()
    {
        pamphletButton.image.sprite = currentDefault;
    }
}
