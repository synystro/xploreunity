using UnityEngine;

public class CreateSlots : MonoBehaviour {

    public GameObject storageSlotPrefab;

    private Storage storage;

    void Awake() {
        // this chest/box/etc's storage script.
        storage = Storage.instance;

        // for each slot create slot
        for (int i = 0; i < storage.slots; i++) {
            GameObject slot = Instantiate(storageSlotPrefab, transform.position, Quaternion.identity);
            slot.transform.SetParent(this.transform);
        }
    }
}
