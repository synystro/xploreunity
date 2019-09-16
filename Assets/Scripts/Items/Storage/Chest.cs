using UnityEngine;

public class Chest : MonoBehaviour {

    public GameObject hud;
    public GameObject inventory;

    private Transform storageUI_T;
    private GameObject storageUI_GO;

    private Storage storage;

    private Animator anim;

    private bool isOpen;

    private void Start()
    {
        storageUI_T = this.transform.GetChild(0);
        storageUI_GO = this.transform.GetChild(0).gameObject;
        storage = GetComponent<Storage>();
        anim = GetComponent<Animator>();

        isOpen = false;
    }

    public void Interact() {

        // MAKE A RANGE CHECK FOR OPENING AT CLOSE RANGE AND CLOSING IT WHEN OUT OF RANGE.

        if (!isOpen) {
            
            if (storage.items.Count > 0) {
                //opens filled animation
                anim.SetInteger("state", 1);
                storageUI_T.SetParent(hud.transform, false);
                storageUI_GO.SetActive(!storageUI_GO.activeSelf);

            } else {
                //opens empty animation.
                anim.SetInteger("state", 3);
                storageUI_T.SetParent(hud.transform, false);
                storageUI_GO.SetActive(!storageUI_GO.activeSelf);
            }
            inventory.SetActive(true);
            isOpen = true;
        } else {
            if(storage.items.Count > 0) {
                //closes filled animation.
                anim.SetInteger("state", 2);
                storageUI_T.SetParent(this.transform, false);
                storageUI_GO.SetActive(!storageUI_GO.activeSelf);
            } else {
                //closes empty animation.
                anim.SetInteger("state", 4);
                storageUI_T.SetParent(this.transform, false);
                storageUI_GO.SetActive(!storageUI_GO.activeSelf);
            }
            inventory.SetActive(false);
            isOpen = false;
        }
    }

    public void FillOrEmpty() {

        if(storage.items.Count > 0) {
            anim.SetInteger("state", 5);
        } else {
            anim.SetInteger("state", 6);
        }

    }

}
