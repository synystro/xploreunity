using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Item item;
    public GameObject itemIconPrefab;
    public bool isTaken;

    private GameObject currentIconGO;
    private Image icon;

    public void AddItem(Item newItem) {
        // skip if there's an item already.
        if (item != null)
            return;
        // add item data.
        item = newItem;
        isTaken = true;
        // set icon.
        icon = itemIconPrefab.GetComponent<Image>();
        // set icon sprite and visibility.
        icon.sprite = item.iconSprite;
        icon.enabled = true;
        // make sure there's no itemGO on this slot's button and then add itemGO to it.
        Transform slotButton = transform.GetChild(0).transform;
        if (slotButton.childCount == 0)
            currentIconGO = Instantiate(itemIconPrefab, transform.GetChild(0).transform);
    }

    public void RemoveItem() {
        // erase item data.
        item = null;
        isTaken = false;
        // get this slot's button.
        Transform slotButton = transform.GetChild(0).transform;
        // if there's an item GO on this slot, destroy it.
        if (slotButton.childCount > 0) {
            GameObject itemGO = slotButton.GetChild(0).gameObject;
            Destroy(itemGO);
        }
    }

    public void SelectItem() {
        // make sure there's an item to select it.
        if (item != null)
            item.Select(this.gameObject);
    }

}
