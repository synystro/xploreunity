﻿using UnityEngine;
using UnityEngine.UI;

public class StorageSlot : MonoBehaviour {
    public Item item;
    public GameObject itemIconPrefab;
    public bool isTaken;

    private GameObject slotIcon;
    private Image icon;

    public void AddItem(Item newItem) {

        if (item == null) {

            item = newItem;
            isTaken = true;

            icon = itemIconPrefab.GetComponent<Image>();

            icon.sprite = item.iconSprite;
            icon.enabled = true;

            if (transform.GetChild(0).childCount == 0) {

                slotIcon = Instantiate(itemIconPrefab, transform.GetChild(0).transform);

            }
        }

    }

    public void RemoveItem() {

        item = null;
        isTaken = false;

        Transform buttonTransform = transform.GetChild(0).transform;

        if (buttonTransform.childCount > 0) {

            Destroy(buttonTransform.GetChild(0).gameObject);

        }
    }

    public void SelectItem() {
        if (item != null) {
            item.Select(this.gameObject);
        }
    }
}