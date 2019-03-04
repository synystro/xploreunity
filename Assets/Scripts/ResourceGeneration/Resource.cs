using UnityEngine;

public class Resource : MonoBehaviour {

    public Item item;

    private int maxAmount;

    public virtual void Extract() {

        //Instantiate(item.itemPrefab, transform.position, Quaternion.identity);

        //Destroy(this.gameObject);

        
        maxAmount = Random.Range(item.minAmount, item.maxAmount + 1);

        bool wasPickedUp = false;

        for (int i = 0; i < maxAmount; i++) {
            //wasPickedUp = Inventory.instance.Add(item);
            Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
            wasPickedUp = true;
        }

        if (wasPickedUp)
            Destroy(this.gameObject);
    }
}
