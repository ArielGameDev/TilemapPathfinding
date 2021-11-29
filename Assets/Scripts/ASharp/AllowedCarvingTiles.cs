using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


public class AllowedCarvingTiles : MonoBehaviour
{
    [SerializeField] TileBase[] allowedCarvingTiles = null;
    public bool Contain(TileBase tile)
    {
        return allowedCarvingTiles.Contains(tile);
    }

    public TileBase[] Get() { return allowedCarvingTiles; }
}
