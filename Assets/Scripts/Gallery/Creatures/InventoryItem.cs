using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Creature Creature {get; set;}

    private InventoryManager inventoryManager;
    private Vector3 originalPosition;
    private Vector3 offset = Vector2.zero;

    void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void SetDropZone()
    {
        if (Creature.CaptureCount < 4)
        {
            inventoryManager.ToggleDropZone(true, ZoneState.Error);
        }

        else
        {
            inventoryManager.ToggleDropZone(true, ZoneState.Default);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Movement
        originalPosition = transform.position;
        offset = GetMousePosition() - transform.position;

        // Drop Zone
        if (inventoryManager.GetCurrentCreatureOnDisplay().CaptureStatus != CreatureStatus.Identified)
        {
            SetDropZone();
            inventoryManager.ToggleDropBlockers(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMousePosition() - offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Movement
        transform.position = originalPosition;

        // Drop Zone
        inventoryManager.ToggleDropZone(false);
        inventoryManager.ToggleDropBlockers(true);
    }

    void OnDestroy()
    {
        // Drop Zone
        inventoryManager.ToggleDropZone(false);
        inventoryManager.ToggleDropBlockers(true);
    }
}
