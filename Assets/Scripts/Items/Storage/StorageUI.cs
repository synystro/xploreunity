using UnityEngine;

public class StorageUI : MonoBehaviour {

    public Transform storageSlotsParent;

    Storage storage;

    public Chest chest;

    StorageSlot[] slots;

    [SerializeField] protected int itemsOnStorage = 0;

    private void Start() {

        // logical storage
        storage = Storage.instance;
        storage.onItemChangedCallback += UpdateUI;
        // this storage's slots
        slots = storageSlotsParent.GetComponentsInChildren<StorageSlot>();

    }

    void UpdateUI() {

        itemsOnStorage = 0;

        for (int i = 0; i < slots.Length; i++) {

            if (slots[i].transform.GetChild(0).childCount > 0) {
                itemsOnStorage++;
            }
        }

        // make WHILE loop here?
        for (int i = 0; i < slots.Length; i++) {
            if (!slots[i].isTaken && itemsOnStorage < storage.items.Count) {
                slots[i].AddItem(storage.items[storage.items.Count - 1]);
                itemsOnStorage++;
            } else if (!slots[i].isTaken) {
                slots[i].RemoveItem();
            }
        }

        // get the chest filled or emptied animation.
        chest.FillOrEmpty();

    }
}