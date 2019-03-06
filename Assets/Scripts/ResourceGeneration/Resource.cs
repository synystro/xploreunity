using UnityEngine;

public class Resource : MonoBehaviour {

    public Item item;

    private int maxAmount;

    public virtual void Extract() {
        
        maxAmount = Random.Range(item.minAmount, item.maxAmount + 1);

        bool wasPickedUp = false;

        for (int i = 0; i < maxAmount; i++) {
            wasPickedUp = Toolbar.instance.Add(item);
            //Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
            wasPickedUp = true;
        }

        if (wasPickedUp)
            Destroy(this.gameObject);
    }
}
