using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentificationZone : MonoBehaviour
{
    private DropZone dropZone;
    private Creature droppedCreature;

    void Start()
    {
        dropZone = GetComponent<DropZone>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        droppedCreature = other.gameObject.GetComponent<InventoryItem>().Creature;

        // Highlight if applicable
        if (droppedCreature.CaptureCount == 4)
        {
            dropZone.Toggle(true, ZoneState.WithinRange);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Reset
        if (dropZone.gameObject.activeSelf)
        {
            if (droppedCreature.CaptureCount < 4)
            {
                dropZone.Toggle(true, ZoneState.Error);
            }
    
            else
            {
                dropZone.Toggle(true, ZoneState.Default);
            }
        }
    }
}
