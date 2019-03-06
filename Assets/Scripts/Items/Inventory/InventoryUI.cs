using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform inventoryParent;

    Inventory inventory;

    InventorySlot[] slots;

    [SerializeField] private int itemsOnInventory = 0;

    private void Start() {

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = inventoryParent.GetComponentsInChildren<InventorySlot>();

    }

    void UpdateUI() {

        itemsOnInventory = 0;

        for (int i = 0; i < slots.Length; i++) {

            if (slots[i].transform.GetChild(0).childCount > 0) {
                itemsOnInventory++;
            }
        }

        // make WHILE loop here?
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