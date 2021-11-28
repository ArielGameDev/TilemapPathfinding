using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using AStarTools;

[System.Serializable]
public class TileDescriptor
{
    [SerializeField]
    public TileBase tile;

    [SerializeField]
    public float moveSpeed;
}