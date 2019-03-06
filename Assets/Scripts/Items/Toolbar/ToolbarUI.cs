using UnityEngine;

public class ToolbarUI : MonoBehaviour {

    public Transform toolbarParent;

    Toolbar toolbar;

    ToolbarSlot[] slots;

    [SerializeField] private int itemsOnToolbar = 0;

    private void Start() {

        toolbar = Toolbar.instance;
        toolbar.onItemChangedCallback += UpdateUI;

        slots = toolbarParent.GetComponentsInChildren<ToolbarSlot>();

    }

    void UpdateUI() {

        itemsOnToolbar = 0;

        for (int i = 0; i < slots.Length; i++) {

            if (slots[i].transform.GetChild(0).childCount > 0) {
                itemsOnToolbar++;
            }
        }

        // make WHILE loop here?
        for (int i = 0; i < slots.Length; i++) {
            if (!slots[i].isTaken && itemsOnToolbar < toolbar.items.Count) {
                slots[i].AddItem(toolbar.items[toolbar.items.Count - 1]);
                itemsOnToolbar++;
            } else if (!slots[i].isTaken) {
                slots[i].RemoveItem();
            }
        }
    }
}