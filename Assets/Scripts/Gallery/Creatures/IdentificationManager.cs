using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentificationManager : MonoBehaviour
{
    [SerializeField] private HallManager hallManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Toast identificationToast;
    [SerializeField] private ProgressTracker progressTracker;

    void OnEnable()
    {
        // Hide toast
        identificationToast.gameObject.SetActive(false);
    }

    public void DestroyInInventory(GameObject item, Creature creature)
    {
        // Remove inventory item from inventory
        Destroy(item);

        // Get inventory index
        int index = inventoryManager.InventoryCreatures.FindIndex(x =>
                    x.ScientificName == creature.ScientificName);

        // Remove canvas group
        inventoryManager.DropBlockers.RemoveAt(index + 1); // 0 is DropZone
        
        // Remove from list
        inventoryManager.InventoryCreatures.RemoveAt(index);

        // If deleted item was selected
        if ((index == (inventoryManager.CurrentPage - 1)) &&
            (inventoryManager.InventoryCreatures.Count > 0))
        {
            // "Next" page if before end
            if (index < inventoryManager.InventoryCreatures.Count)
            {
                inventoryManager.Jump(index + 1); // Page starts at 1
            }
    
            // "Prev" page if end
            else if (index >= inventoryManager.InventoryCreatures.Count)
            {
                inventoryManager.Jump(index);
            }
        }
    }

    public IEnumerator ShowResult(bool matchResult)
    {
        ShowToast(matchResult);

        yield return new WaitForSeconds(2f);

        // Hide
        identificationToast.gameObject.SetActive(false);
    }

    public void UpdateTracker()
    {
        progressTracker.AddOne();
    }

    public void SetIdentifiedTOCItem()
    {
        hallManager.RebuildTOCItemOfCurrentPage();
    }

    private void ShowToast(bool isMatch)
    {
        // Success
        if(isMatch)
        {
            identificationToast.SetToast(0);
        }

        // Fail
        else
        {
            identificationToast.SetToast(1);
        }

        identificationToast.gameObject.SetActive(true);
    }
}
