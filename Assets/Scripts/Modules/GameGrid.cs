using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameGrid : MonoBehaviour
{
    public Vector2Int Size;
    public float TileSize;
    
    public static GameGrid Instance;
    public Tilemap tilemap;
    
    [ItemCanBeNull] public Plant[,] Plants;
    
    private void Awake()
    {
        Instance = this;
        
        tilemap = GetComponentInChildren<Tilemap>();
        
        Plants = new Plant[Size.x, Size.y];
    }

    public int GetRandomRow()
    {
        return Random.Range(0, Size.y);
    }

    public Vector3 GetGlobalPosition(Vector2Int position)
    {
        return transform.position + new Vector3(
            (position.x) * TileSize - TileSize / 2,
            (position.y) * TileSize - TileSize / 2,
            0
        );
    }
}
