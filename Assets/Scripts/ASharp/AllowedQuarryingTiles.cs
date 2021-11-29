using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


public class AllowedQuarryingTiles : MonoBehaviour
{
    [SerializeField] TileBase[] allowedQuarryingTiles = null;
    public bool Contain(TileBase tile)
    {
        return allowedQuarryingTiles.Contains(tile);
    }

    public TileBase[] Get() { return allowedQuarryingTiles; }
}
