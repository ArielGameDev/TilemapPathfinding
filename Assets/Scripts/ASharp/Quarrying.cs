using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Quarrying : MonoBehaviour
{
    [SerializeField] private KeyCode quarryingKey;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase changeToTile;
    [SerializeField] private AllowedQuarryingTiles allowedQuarryingTiles;
    [SerializeField] private Sprite quarryingSprite;
    [SerializeField] private float time = .75f;
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
        if (Input.GetKeyDown(quarryingKey))
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
            
            if (allowedQuarryingTiles.Contain(tile)) StartCoroutine(ChangeTile(cellPosition));
            else Debug.Log("You cannot quarrying " + tile);   
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
        spriteRenderer.sprite = quarryingSprite;
        yield return new WaitForSeconds(time);
        tilemap.SetTile(cellPosition, changeToTile);
        spriteRenderer.sprite = startingSprite;
    }
}
