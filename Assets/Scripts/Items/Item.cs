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

        //Debug.Log("Selecting " + name);
        if (itemPrefab.GetComponent<PlaceableBlock>()) {
            itemPrefab.GetComponent<PlaceableBlock>().Grab(blockPrefab, slot);
        }
    }
    
    public void RemoveFromIventory() {

        Inventory.instance.Remove(this);
    }
    
}
