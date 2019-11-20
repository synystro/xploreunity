using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public Material material;

    [SerializeField] private int width;
    [SerializeField] private int height;

    public Tile[,] tiles;

    [Header("Noise")]

    Noise noise;

    [SerializeField] public string seed;
    [SerializeField] bool randomSeed;

    [SerializeField] float frequency;
    [SerializeField] float amplitude;

    // modifies frequency.
    [SerializeField] float lacunarity;
    // modifies amplitude.
    [SerializeField] float persistance;

    [SerializeField] int octaves;

    [Header("Chunk")]
    [SerializeField] public int chunkSize;

    public List<GameObject> chunks = new List<GameObject>();

    [Header("Tile Levels")]

    [SerializeField] private float seaLevel;
    [SerializeField] private float beachSandStartHeight;
    [SerializeField] private float beachSandEndHeight;

    [SerializeField] private float dirtStartHeight;
    [SerializeField] private float dirtEndHeight;

    [SerializeField] private float grassStartHeight;
    [SerializeField] private float grassEndHeight;

    [SerializeField] private float stoneStartHeight;
    [SerializeField] private float stoneEndHeight;

    public int Width { get { return width; } }
    public int Height { get { return height; } }

    #region Singleton

    public static Planet instance;

    private void Awake() {
        if (instance != null) {
            Debug.Log("More than one instance of Planet found.");
            return;
        }
        instance = this;
    }
    #endregion

    void Start() {

        #region build noise from random seed
        if (randomSeed) {
            seed = Random.Range(-9999, 9999).ToString();
        }
        noise = new Noise(int.Parse(seed), frequency, amplitude, lacunarity, persistance, octaves);
        #endregion

        CreateTiles();

        SubdivideTilesArray();

    }

    void CreateTiles() {

        tiles = new Tile[width, height];

        float[,] noiseValues = noise.GetNoiseValues(width, height);

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {

                // generate tile.
                tiles[i, j] = MakeTileAtHeight(noiseValues[i, j]);
                // generate resource?
                ResourceGenerator.instance.GenerateResource(i, j);

            }
        }
    }

    Tile MakeTileAtHeight(float currentHeight) {

        if (currentHeight <= seaLevel)
            return new Tile(Tile.Type.Water);
        if (currentHeight >= beachSandStartHeight && currentHeight <= beachSandEndHeight)
            return new Tile(Tile.Type.Sand);
        if (currentHeight >= dirtStartHeight && currentHeight <= dirtEndHeight)
            return new Tile(Tile.Type.Dirt);
        if (currentHeight >= grassStartHeight && currentHeight <= grassEndHeight)
            return new Tile(Tile.Type.Grass);
        if (currentHeight >= stoneStartHeight && currentHeight <= stoneEndHeight)
            return new Tile(Tile.Type.Stone);

        return new Tile(Tile.Type.Void);

        
    }

    void SubdivideTilesArray(int i1 = 0, int i2 = 0) {

        if (i1 > tiles.GetLength(0) && i2 > tiles.GetLength(1))
            return;

        //Get size of segment
        int sizeX, sizeY;

        if (tiles.GetLength(0) - i1 > chunkSize) {

            sizeX = chunkSize;
        } else
            sizeX = tiles.GetLength(0) - i1;

        if (tiles.GetLength(1) - i2 > chunkSize) {

            sizeY = chunkSize;
        } else
            sizeY = tiles.GetLength(1) - i2;

        GenerateChunkMesh(i1, i2, sizeX, sizeY);

        if (tiles.GetLength(0) > i1 + chunkSize) {
            SubdivideTilesArray(i1 + chunkSize, i2);
            return;
        }

        if (tiles.GetLength(1) > i2 + chunkSize) {
            SubdivideTilesArray(0, i2 + chunkSize);
            return;
        }
    }

    void GenerateChunkMesh(int x, int y, int width, int height) {

        MeshData data = new MeshData(x, y, width, height);

        // create chunk GO then parent it to Planet.

        GameObject chunkMeshGO = new GameObject("CHUNK_" + (chunks.Count + 1) + "_" + x + "_" + y);
        chunkMeshGO.transform.SetParent(this.transform);

        // create resourcesGO, add resources to it, and then parent resourcesGO to chunk.

        GameObject resourcesGO = new GameObject("Resources");
        resourcesGO.transform.SetParent(chunkMeshGO.transform);

        List<GameObject> resources = ResourceGenerator.instance.resources;
        for(int i = 0; i < resources.Count; i++) {
            if (resources[i].transform.position.x >= x && resources[i].transform.position.x <= x + width) {
                if (resources[i].transform.position.y >= y && resources[i].transform.position.y <= y + height) {
                    resources[i].transform.SetParent(resourcesGO.transform);
                }
            }
        }

        // chunk resources disabled by default.
        resourcesGO.SetActive(false);

        // add chunk to chunks array.
        chunks.Add(chunkMeshGO);

        MeshFilter filter = chunkMeshGO.AddComponent<MeshFilter>();
        MeshRenderer render = chunkMeshGO.AddComponent<MeshRenderer>();
        render.material = material;

        Mesh mesh = filter.mesh;

        mesh.vertices = data.vertices.ToArray();
        mesh.triangles = data.triangles.ToArray();
        mesh.uv = data.UVs.ToArray();

        // add box collider.
        chunkMeshGO.AddComponent<BoxCollider2D>();

        // inactive by default
        render.enabled = false;
    }

    public Tile GetTileAt(int x, int y) {

        return x < 0 || x >= width || y < 0 || y >= height ? new Tile(Tile.Type.Void) : tiles[x, y];
    }
}
