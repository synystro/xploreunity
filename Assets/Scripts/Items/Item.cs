using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public GameObject itemPrefab;
    public GameObject blockPrefab;

    new public string name = "New Item";
    public int id = 0;
    public Sprite iconSprite;
    public int maxAmount = 0;
    public int minAmount = 0;

    public virtual void Select(GameObject slot) {
        // select item.

        // check if its a placeable block, if yes then grab it.
        if (itemPrefab.GetComponent<PlaceableBlock>())
            itemPrefab.GetComponent<PlaceableBlock>().Grab(blockPrefab, slot);
    }
    
    public void RemoveFromIventory() {
        // remove item from inventory instance.
        Inventory.instance.Remove(this);
    }
    
}
