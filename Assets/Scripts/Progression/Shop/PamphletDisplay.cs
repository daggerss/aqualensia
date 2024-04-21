using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PamphletDisplay : MonoBehaviour
{
    [SerializeField]
    private ParentBlocker _blocker;
    public ParentBlocker Blocker => _blocker;

    [Header("UI")]
    [SerializeField] private Image blockerImage;
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

    void Start()
    {
        // Get UI components
        pamphletImage = GetComponent<Image>();
        pamphletButton = GetComponent<Button>();

        // Set display and button assets
        if (_blocker != null)
        {
            SetDisplay();
        }
    }

    // Set display according to blocker
    private void SetDisplay()
    {
        // Blocker
        blockerImage.sprite = _blocker.Sprite;

        // Button States
        if (_blocker.Biome == Biome.CoralReef)
        {
            ChangeSpriteSwap(coralReefDefault, coralReefState);
        }

        else if (_blocker.Biome == Biome.SeagrassBed)
        {
            ChangeSpriteSwap(seagrassDefault, seagrassState);
        }

        else if (_blocker.Biome == Biome.OpenOcean)
        {
            ChangeSpriteSwap(openOceanDefault, openOceanState);
        }
    }

    // Apply sprite states to button
    private void ChangeSpriteSwap(Sprite normal, SpriteState sprState)
    {
        // Default
        pamphletButton.image.sprite = normal;

        // States
        pamphletButton.spriteState = sprState;
    }
}
