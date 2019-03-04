using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    public enum Type { Dirt, Grass, Sand, Stone, Water, Void }
    public Type type;
    public Vector3 pos;
    public bool isEmpty;

    public Tile(Type type) {

        this.type = type;
    }

    public void SetTile(Type newType) {
        type = newType;
    }
}
