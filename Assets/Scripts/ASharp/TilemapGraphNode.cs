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

    List<TileDescriptor> m_tileDescriptors;

    public TilemapGraphNode(Tilemap tilemap, TileBase[] allowedTiles, List<TileDescriptor> tileDescriptors)
    {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
        m_tileDescriptors = tileDescriptors;
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


            TileBase currentTile = tilemap.GetTile(neighborPos);
            
            // lazily find correct tile
            foreach (TileDescriptor tile in m_tileDescriptors)
            {
                if (tile.tile == currentTile)
                    newNode.travelWeight = 1/tile.moveSpeed;
            }


            if (allowedTiles.Contains(neighborTile))
                yield return newNode;
        }
    }

}
