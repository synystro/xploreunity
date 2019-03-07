using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    // dragged item and item GO.
    public static GameObject draggedItemGO;
    public static Item draggedItem;

    public static InventorySlot inventorySlot;
    public static StorageSlot storageSlot;

    public static bool aSwapped;

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

        if (slotParent.GetComponent<InventorySlot>()) { inventorySlot = slotParent.GetComponent<InventorySlot>(); } else if (slotParent.GetComponent<StorageSlot>()) { storageSlot = slotParent.GetComponent<StorageSlot>(); }

        draggedItemGO = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        canvasGroup.blocksRaycasts = false;

        transform.SetParent(transform.root);

        if (inventorySlot != null) { draggedItem = inventorySlot.item; }
        else if (storageSlot != null) { draggedItem = storageSlot.item; }

    }

    public void OnDrag(PointerEventData eventData) {

        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData) {

        canvasGroup.blocksRaycasts = true;

        // if outside inventory/storage panel parent.
        if (transform.parent == startParent || transform.parent == transform.root) {

            transform.position = startPosition;
            transform.SetParent(startParent);
            //if(inventorySlot != null) { inventorySlot.isTaken = true; }
            //else if(storageSlot != null) { storageSlot.isTaken = true; }

        } else {
            // if outside its original position and inside a slot remove it from its original slot.
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