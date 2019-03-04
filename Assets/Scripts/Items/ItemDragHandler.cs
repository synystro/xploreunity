﻿using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    // dragged item and item GO.
    public static GameObject draggedItemGO;
    public static Item draggedItem;

    public static InventorySlot inventorySlot;
    public static ToolbarSlot toolbarSlot;

    private GameObject slotParent;

    Vector3 startPosition;
    Transform startParent;
    CanvasGroup canvasGroup;

    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {

        GameObject button = transform.parent.gameObject;
        slotParent = button.transform.parent.gameObject;

        if (slotParent.GetComponent<InventorySlot>()) { inventorySlot = slotParent.GetComponent<InventorySlot>(); }
        else if (slotParent.GetComponent<ToolbarSlot>()) { toolbarSlot = slotParent.GetComponent<ToolbarSlot>(); }

        draggedItemGO = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        canvasGroup.blocksRaycasts = false;

        transform.SetParent(transform.root);

        if (inventorySlot != null) { draggedItem = inventorySlot.item; }
        else if (toolbarSlot != null) { draggedItem = toolbarSlot.item; }

    }

    public void OnDrag(PointerEventData eventData) {

        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData) {

        draggedItem = null;
        draggedItemGO = null;

        canvasGroup.blocksRaycasts = true;

        // if outside inventory/toolbar panel parent.
        if (transform.parent == startParent || transform.parent == transform.root) {

            transform.position = startPosition;
            transform.SetParent(startParent);

        } else {
            // if outside its original position and inside a slot remove it from its original slot.
            if (inventorySlot != null) {
                inventorySlot.RemoveItem();
                inventorySlot.isTaken = false;
                Inventory.instance.Remove(inventorySlot.item);
            } else if(toolbarSlot != null) {
                toolbarSlot.RemoveItem();
                toolbarSlot.isTaken = false;
                Toolbar.instance.Remove(toolbarSlot.item);
            }
        }

        // reset slotParent reference;

        inventorySlot = null;
        toolbarSlot = null;

    }
}