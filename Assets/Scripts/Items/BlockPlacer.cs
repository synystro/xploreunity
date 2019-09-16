using UnityEngine;

public class BlockPlacer : MonoBehaviour {

    public GameObject block;
    public GameObject slot;
    
    public bool isActive;

    private GameObject slotIcon;
    private Vector2 mousePos;
    private Vector3 offset;
    private float offsetValue = -0.5f;
    private Vector2 alignedToGrid;
    private SpriteRenderer spriteRenderer;
    private Inventory inventory;

    private void Start() {

        inventory = Inventory.instance;

        // offset for the block to be put at mouseposition.
        offset = new Vector3(offsetValue, offsetValue, 0);

        spriteRenderer = GetComponent<SpriteRenderer>();
        isActive = false;
    }

    void Update() {

        if (isActive) {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            alignedToGrid = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));

            this.transform.position = alignedToGrid;

            spriteRenderer.sprite = block.GetComponent<SpriteRenderer>().sprite;


            if (Input.GetMouseButton(1)) {

                spriteRenderer.sprite = null;
                isActive = false;
                return;
            } else if (Input.GetMouseButton(0)) {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mouseRay = Camera.main.ScreenToWorldPoint(transform.position);
                RaycastHit2D rayHit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity);

                slotIcon = slot.transform.GetChild(0).transform.GetChild(0).gameObject;

                if (rayHit.collider.gameObject.layer != block.layer && slotIcon != null && rayHit.collider.gameObject.layer != LayerMask.GetMask("UI")) {

                    // create block.
                    Instantiate(block, alignedToGrid, Quaternion.identity);
                    // remove item from inventory.
                    inventory.Remove(slot.GetComponent<InventorySlot>().item);
                    // get slot component.
                    InventorySlot slotScript = slot.GetComponent<InventorySlot>();
                    // remove item from slot, free slot and destroy the slotItemIcon
                    slotScript.item = null;
                    slotScript.isTaken = false;
                    Destroy(slotIcon);
                    // remove sprite from block placer and deactivate it.
                    spriteRenderer.sprite = null;
                    isActive = false;
                    return;
                    
                    // subtract block's resource item from inventory.
                }
            }
        }
    }
}