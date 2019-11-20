using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    // dragged item and item GO.
    public static GameObject draggedItemGO;
    public static Item draggedItem;

    public static InventorySlot inventorySlot;
    public static StorageSlot storageSlot;

    public static bool aSwapped;

    Vector3 originalPosition;
    Transform originalParent;
    CanvasGroup canvasGroup;

    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        // set buttonGO GO.
        GameObject buttonGO = transform.parent.gameObject;
        // set slot GO.
        GameObject slotGO = buttonGO.transform.parent.gameObject;

        // if it's from inventory.
        if (slotGO.GetComponent<InventorySlot>())
            inventorySlot = slotGO.GetComponent<InventorySlot>();
        // if it's from storage (i.e. chest, box, etc)
        else if (slotGO.GetComponent<StorageSlot>())
            storageSlot = slotGO.GetComponent<StorageSlot>();

        // set this gameobject as the dragged item.
        draggedItemGO = gameObject;
        // check if it's an inventory item.
        if (inventorySlot != null)
            draggedItem = inventorySlot.item;
        // check if it's a storage item.
        else if (storageSlot != null)
            draggedItem = storageSlot.item;

        // remember its start position.
        originalPosition = transform.position;
        // remember its original parent.
        originalParent = transform.parent;
        // canvas allows raycasts.
        canvasGroup.blocksRaycasts = false;

        // detach it from its original parent and set the HUD as its parent.
        transform.SetParent(transform.root);

    }

    public void OnDrag(PointerEventData eventData) {
        // drag this item to mouse position.
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {

        // canvas blocks raycasts again.
        canvasGroup.blocksRaycasts = true;

        // if its current parent is its original parent or the HUD, move it back to its original slot.
        if (transform.parent == originalParent || transform.parent == transform.root) {

            transform.position = originalPosition;
            transform.SetParent(originalParent);

        } else {
            // if outside its original position and inside an empty slot, remove it from its original slot.
            if (inventorySlot != null && !aSwapped) {
                    inventorySlot.RemoveItem();
                    inventorySlot.isTaken = false;
                    Inventory.instance.Remove(inventorySlot.item);
            } else if (storageSlot != null && !aSwapped) {
                    storageSlot.RemoveItem();
                    storageSlot.isTaken = false;
                    Storage.instance.Remove(storageSlot.item);
            }
        }

        // reset static variables for next item drag;
        draggedItem = null;
        draggedItemGO = null;
        inventorySlot = null;
        storageSlot = null;
        aSwapped = false;

    }
}