using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Carve : MonoBehaviour
{
    [SerializeField] private KeyCode carvingKey;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase changeToTile;
    [SerializeField] private AllowedCarvingTiles allowedCarvingTiles;
    [SerializeField] private Sprite carvingSprite;
    [SerializeField] private float time = 1f;
    private SpriteRenderer spriteRenderer;
    private Sprite startingSprite;
    private enum Direction { up, down, right, left , zero }
    Direction direction;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startingSprite = spriteRenderer.sprite;
        direction = Direction.zero;
    }
    private void Update()
    {
        if (Input.GetKeyDown(carvingKey))
        {
            Vector3 dir = Vector3.zero;
            switch (ArrowDirection())
            {
                case Direction.up:
                    dir = Vector3.up;
                    break;
                case Direction.down:
                    dir = Vector3.down;
                    break;
                case Direction.left:
                    dir = Vector3.left;
                    break;
                case Direction.right:
                    dir = Vector3.right;
                    break;
            }
            dir += transform.position;
            Vector3Int cellPosition = tilemap.WorldToCell(dir);
            TileBase tile = tilemap.GetTile(cellPosition);
            
            if (allowedCarvingTiles.Contain(tile)) StartCoroutine(ChangeTile(cellPosition));
            else Debug.Log("You cannot carving " + tile);   
        }
    }
    
    private Direction ArrowDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow)) direction = Direction.up;
        else if (Input.GetKey(KeyCode.DownArrow)) direction = Direction.down;
        else if (Input.GetKey(KeyCode.RightArrow)) direction = Direction.right;
        else if (Input.GetKey(KeyCode.LeftArrow)) direction = Direction.left;
        else direction = Direction.zero;
        
        return direction;
    }
    private IEnumerator ChangeTile(Vector3Int cellPosition)
    {
        spriteRenderer.sprite = carvingSprite;
        yield return new WaitForSeconds(time);
        tilemap.SetTile(cellPosition, changeToTile);
        spriteRenderer.sprite = startingSprite;
    }
}
