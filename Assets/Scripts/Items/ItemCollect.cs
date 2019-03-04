using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    public Item item;

    [SerializeField] private float pickUpItemScale = 0.3f;

    private void Awake() {

        transform.localScale = new Vector3(pickUpItemScale, pickUpItemScale, 1);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<Player>()) {
            Inventory.instance.Add(item);
            Destroy(this.gameObject);
        }
    }
}
