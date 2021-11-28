using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using AStarTools;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */
public class TilemapGraphNode : IGraph<Node>
{
    private Tilemap tilemap;
    private TileBase[] allowedTiles;

    public TilemapGraphNode(Tilemap tilemap, TileBase[] allowedTiles)
    {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
    }

    static Vector3Int[] directions = {
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 1, 0),
    };

    public IEnumerable<Node> Neighbors(Node node)
    {
        foreach (var direction in directions)
        {
            Vector3Int neighborPos = node.coordinates + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);

            Node newNode = new Node(neighborPos);

            if (allowedTiles.Contains(neighborTile))
                yield return newNode;
        }
    }

}
