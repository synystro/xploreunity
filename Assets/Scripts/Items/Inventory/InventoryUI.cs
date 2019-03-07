using UnityEngine;

public class InventoryUI : MonoBehaviour {

    Inventory inventory;

    [SerializeField] InventorySlot[] slots;

    [SerializeField] private int itemsOnInventory = 0;

    private void Start() {

        // get slots from this script's GO children (even if inventory's GO is disabled).
        slots = GetComponentsInChildren<InventorySlot>(true);

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
    }

    void UpdateUI() {

        itemsOnInventory = 0;

        for (int i = 0; i < slots.Length; i++) {

            if (slots[i].transform.GetChild(0).childCount > 0) {
                itemsOnInventory++;
            }
        }

        for (int i = 0; i < slots.Length; i++) {
            if (!slots[i].isTaken && itemsOnInventory < inventory.items.Count) {
                slots[i].AddItem(inventory.items[inventory.items.Count - 1]);
                itemsOnInventory++;
            } else if (!slots[i].isTaken) {
                slots[i].RemoveItem();
            }
        }
    }
}