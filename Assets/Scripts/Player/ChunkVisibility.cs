using UnityEngine;

public class ChunkVisibility : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.name.Contains("CHUNK")) {
            col.GetComponent<MeshRenderer>().enabled = true;
            col.transform.Find("Resources").gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.name.Contains("CHUNK")) {
            col.GetComponent<MeshRenderer>().enabled = false;
            col.transform.Find("Resources").gameObject.SetActive(false);
        }
    }
}
