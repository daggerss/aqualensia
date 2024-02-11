using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventoryManager : SwipeController
{
    [Header("Photo Info")]
    [SerializeField] private GameObject selectorClip;
    [SerializeField] private Image creatureImage;
    [SerializeField] private GameObject[] trackerPoints;
    [SerializeField] private Transform bulletsParent;
    [SerializeField] private GameObject bulletItemPrefab;

    [Header("Drag & Drop")]
    [SerializeField] private DropZone displayDropZone;
    [SerializeField] private SwipeController displaySwipe;
    public List<CanvasGroup> DropBlockers = new List<CanvasGroup>();

    [HideInInspector]
    public List<Creature> InventoryCreatures = new List<Creature>();

    [HideInInspector]
    public Creature[] DisplayCreatures;

    void Start()
    {
        // Hide paperclip
        if (!(InventoryCreatures?.Any() ?? false)) // If empty
        {
            selectorClip.SetActive(false);
        }

        // Set initial
        else
        {
            SetAllInfo(InventoryCreatures[0]);
        }

        // Get item canvas groups
        foreach (Transform item in ContentRect)
        {
            DropBlockers.Add(item.gameObject.GetComponent<CanvasGroup>());
        }
    }

    public override void MovePage()
    {
        base.MovePage();
        SetAllInfo(InventoryCreatures[CurrentPage - 1]); // Pages start at 1
    }

    /* ----------------------------- Drag & Drop ---------------------------- */

    public void ToggleDropZone(bool visible, ZoneState state = ZoneState.Default)
    {
        displayDropZone.Toggle(visible, state);
    }

    public void ToggleDropBlockers(bool toBlock)
    {
        foreach (CanvasGroup ui in DropBlockers)
        {
            ui.blocksRaycasts = toBlock;
        }
    }

    public Creature GetCurrentCreatureOnDisplay()
    {
        return DisplayCreatures[displaySwipe.CurrentPage - 1];
    }

    /* ----------------------------- Photo Info ----------------------------- */
    private void SetAllInfo(Creature c)
    {
        // Image
        SetImage(c.Sprite);

        // Tracker
        SetTracker(c.CaptureCount);

        // Bullets
        SetBullets(c.CaptureCount, c.PhotoInfo);
    }

    private void SetImage(Sprite img)
    {
        creatureImage.sprite = img;
        creatureImage.color = Color.white;
    }

    // Show count number of tracker points
    private void SetTracker(int count)
    {
        for(int i = 0; i < 4; i++)
        {
            if (i < count)
            {
                trackerPoints[i].SetActive(true);
            }

            else
            {
                trackerPoints[i].SetActive(false);
            }
        }
    }

    // Create + set bullets
    private void SetBullets(int count, string[] info)
    {
        // Reset
        foreach (Transform child in bulletsParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        // Revealed
        for (int i = 0; i < count; i++)
        {
            // Instantiate GO
            GameObject newBulletItem = Instantiate(bulletItemPrefab, bulletsParent);
            
            // Change text
            if (newBulletItem.TryGetComponent<BulletItem>(out BulletItem item))
            {
                item.SetText(info[i]);
            }
        }

        // Remaining
        if (count != 4)
        {
            // Instantiate GO
            GameObject noticeBulletItem = Instantiate(bulletItemPrefab, bulletsParent);
            
            // Change text
            if (noticeBulletItem.TryGetComponent<BulletItem>(out BulletItem notice))
            {
                notice.SetEmphasizedText("Capture " + (4 - count) + " more for further observations.");
            }
        }
    }
}
