using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public GameObject itemPrefab;

    new public string name = "New Item";
    public int id = 0;
    public Sprite iconSprite;
    public int maxAmount = 0;
    public int minAmount = 0;

    public virtual void Use() {
        // use item.
        // effect/action.

        Debug.Log("Using " + name);
    }
    
    public void RemoveFromIventory() {

        Inventory.instance.Remove(this);
    }
    
}
