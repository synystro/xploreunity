using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour {

    #region Singleton

    public static Toolbar instance;

    private void Awake() {
        if (instance != null) {
            Debug.Log("More than one instance of Inventory found.");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int slots = 9;

    public List<Item> items = new List<Item>();

    public bool Add(Item item) {

        if (items.Count >= slots) {

            // try to add to the inventory
            if (Inventory.instance.Add(item)) {
                return true;
            } else {
                return false;
            }
        }

        items.Add(item);

        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item item) {

        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

    }
}
