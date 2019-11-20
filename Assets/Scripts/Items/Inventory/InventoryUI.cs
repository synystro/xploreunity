using UnityEngine;

public class InventoryUI : MonoBehaviour {

    Inventory inventory;

    [SerializeField] InventorySlot[] slots;

    [SerializeField] private int itemsOnInventoryUI = 0;

    private void Start() {
        // get slots from this script's GO children (even if inventory's GO is disabled).
        slots = GetComponentsInChildren<InventorySlot>(true);
        // get inventory instance and add updateUI to its on item change callback.
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
    }

    void UpdateUI() {
        // clear items on UI.
        itemsOnInventoryUI = 0;

        // check all slots for items and add them to the Inventory Item's UI counter.
        for (int i = 0; i < slots.Length; i++) {
            // "childcount > 0" means that there's an item on the current slot.
            if (slots[i].transform.GetChild(0).childCount > 0) {
                itemsOnInventoryUI++;
            }
        }
        // check all slots and refresh their visibility.
        for (int i = 0; i < slots.Length; i++) {
            // if the slot is empty and there's an item from the inventory to add to the UI.
            if (!slots[i].isTaken && itemsOnInventoryUI < inventory.items.Count) {
                // add next item from inventory to the UI.
                slots[i].AddItem(inventory.items[inventory.items.Count - 1]);
                itemsOnInventoryUI++;
            // if the current slot is now empty, make sure to remove any item from it.
            } else if (!slots[i].isTaken) {
                slots[i].RemoveItem();
            }
        }
    }

}