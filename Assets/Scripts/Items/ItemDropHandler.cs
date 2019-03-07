using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler {

    private InventorySlot inventorySlot;
    private StorageSlot storageSlot;

    Inventory inventory;
    Storage toolbar;

    private void Start() {
        inventory = Inventory.instance;
        toolbar = Storage.instance;
    }

    public GameObject itemGO {
        get {
            if (transform.childCount > 0) {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData) {

        if (transform.parent.GetComponent<InventorySlot>()) { inventorySlot = GetComponentInParent<InventorySlot>(); }
        else if (transform.parent.GetComponent<StorageSlot>()) { storageSlot = GetComponentInParent<StorageSlot>(); }

        // if this script's GO is an inventorySlot or else if this script's GO is a storageSlot.
        if (inventorySlot != null) {

            // if empty inventory slot.
            if (!itemGO) {

                ItemDragHandler.draggedItemGO.transform.SetParent(this.transform);
                inventorySlot.item = ItemDragHandler.draggedItem;
                inventorySlot.isTaken = true;

                // if adding from toolbar.
                if (ItemDragHandler.storageSlot != null) {
                    inventory.Add(ItemDragHandler.storageSlot.item);
                    toolbar.Remove(ItemDragHandler.storageSlot.item);
                }

            } else {

                // swap
                ItemDragHandler.aSwapped = true;

                if (ItemDragHandler.storageSlot != null) {

                    // swap from inventory to toolbar slot.
                    SwapFromInventoryToToolbar();

                } else if(ItemDragHandler.inventorySlot != null) {

                    // swap between inventory slots.
                    ItemDragHandler.draggedItemGO.transform.SetParent(this.transform);
                    inventorySlot.transform.GetChild(0).transform.GetChild(0).transform.SetParent(ItemDragHandler.inventorySlot.transform.GetChild(0));

                }
            }

        } else if (storageSlot != null) {

            // if empty toolbar slot.
            if (!itemGO) {

                ItemDragHandler.draggedItemGO.transform.SetParent(this.transform);
                storageSlot.item = ItemDragHandler.draggedItem;
                storageSlot.isTaken = true;

                // if adding from inventory.
                if(ItemDragHandler.inventorySlot != null) {
                    toolbar.Add(ItemDragHandler.inventorySlot.item);
                    inventory.Remove(ItemDragHandler.inventorySlot.item);
                }
;
            } else {

                // swap
                ItemDragHandler.aSwapped = true;

                // if swapping to inventory.
                if (ItemDragHandler.inventorySlot != null) {

                    SwapFromToolbarToInventory();

                } else if (ItemDragHandler.storageSlot != null) {

                    // swap between toolbar slots.
                    ItemDragHandler.draggedItemGO.transform.SetParent(this.transform);
                    storageSlot.transform.GetChild(0).transform.GetChild(0).transform.SetParent(ItemDragHandler.storageSlot.transform.GetChild(0));

                }
            }
        }
    }

    private void SwapFromInventoryToToolbar() {

        // add this inventorySlot's child GO to draghandler's toolbarsLot's child transform.
        inventorySlot.transform.GetChild(0).transform.GetChild(0).transform.SetParent(ItemDragHandler.storageSlot.transform.GetChild(0));

        // add toolbar's draggeditem to this inventorySlot's child.
        ItemDragHandler.draggedItemGO.transform.SetParent(this.transform);

        // actual inventory/toolbar swap.
        inventory.Remove(inventorySlot.item);
        inventory.Add(ItemDragHandler.storageSlot.item);
        toolbar.Remove(ItemDragHandler.storageSlot.item);
        toolbar.Add(inventorySlot.item);

        // exchange slots' slotted items. (maybe make this better later on)
        ItemDragHandler.storageSlot.item = inventorySlot.item;
        inventorySlot.item = ItemDragHandler.draggedItem;

    }

    private void SwapFromToolbarToInventory() {

        // swap from toolbar to inventory slot.

        // add this storageSlot's child GO to draghandler's inventorySlot's child transform.
        storageSlot.transform.GetChild(0).transform.GetChild(0).transform.SetParent(ItemDragHandler.inventorySlot.transform.GetChild(0));

        // add inventory's draggeditem to this storageSlot's child.
        ItemDragHandler.draggedItemGO.transform.SetParent(this.transform);

        // actual inventory/toolbar swap.
        toolbar.Remove(storageSlot.item);
        toolbar.Add(ItemDragHandler.inventorySlot.item);
        inventory.Remove(ItemDragHandler.inventorySlot.item);
        inventory.Add(storageSlot.item);

        // exchange slots' slotted items. (maybe make this better later on)
        ItemDragHandler.inventorySlot.item = storageSlot.item;
        storageSlot.item = ItemDragHandler.draggedItem;

    }
}