using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {
    public static ResourceGenerator instance;

    [Header("Prefabs")]
    public List<GameObject> prefabs = new List<GameObject>();

    [Header("Rarity")]
    [SerializeField] private float diamondChance;
    [SerializeField] private float treeChance;

    [Header("Generated Resources")]
    public List<GameObject> resources = new List<GameObject>();

    // variables to store map's size.
    private int width;
    private int height;

    private void Awake() {
        instance = this;

        // store map's size.
        width = Planet.instance.Width;
        height = Planet.instance.Height;
    }

    private void Start() {

    }

    public void GenerateResource(int x, int y) {

        if (Random.Range(0f, 100f) < diamondChance) {
            resources.Add(Instantiate(prefabs[0], new Vector2(x, y), Quaternion.identity));
            resources[(resources.Count - 1)].transform.SetParent(this.transform);
            return;
        }
        if (Random.Range(0f, 100f) < treeChance && Planet.instance.GetTileAt(x,y).type == Tile.Type.Grass) {
            resources.Add(Instantiate(prefabs[1], new Vector2(x, y), Quaternion.identity));
            resources[(resources.Count - 1)].transform.SetParent(this.transform);
            return;
        }
    }
}
