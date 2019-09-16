using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableBlock : MonoBehaviour {
    private GameObject blockPlacer;
    private BlockPlacer blockPlacerScript;

    public void Grab(GameObject blockPrefab, GameObject slot) {

        blockPlacer = GameObject.Find("BlockPlacer");
        blockPlacerScript = blockPlacer.GetComponent<BlockPlacer>();

        blockPlacerScript.block = blockPrefab;
        blockPlacerScript.slot = slot;

        blockPlacerScript.isActive = true;
    }

}
