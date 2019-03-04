using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler {

    private InventorySlot inventorySlot;
    private ToolbarSlot toolbarSlot;

    Inventory inventory;
    Toolbar toolbar;

    private void Start() {
        inventory = Inventory.instance;
        toolbar = Toolbar.instance;
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
        if (transform.parent.GetComponent<ToolbarSlot>()) { toolbarSlot = GetComponentInParent<ToolbarSlot>(); }

        if (inventorySlot != null) {

            // if empty slot.
            if (!itemGO) {

                ItemDragHandler.draggedItemGO.transform.SetParent(this.transform);
                inventorySlot.item = ItemDragHandler.draggedItem;
                inventorySlot.isTaken = true;

                if (ItemDragHandler.toolbarSlot != null) {
                    inventory.Add(ItemDragHandler.toolbarSlot.item);
                    toolbar.Remove(ItemDragHandler.toolbarSlot.item);
                }

            }

        } else if (toolbarSlot != null) {

            // if empty slot.
            if (!itemGO) {

                ItemDragHandler.draggedItemGO.transform.SetParent(this.transform);
                toolbarSlot.item = ItemDragHandler.draggedItem;
                toolbarSlot.isTaken = true;

                if(ItemDragHandler.inventorySlot != null) {
                    toolbar.Add(ItemDragHandler.inventorySlot.item);
                    inventory.Remove(ItemDragHandler.inventorySlot.item);
                }
;
            }
        }
    }
}