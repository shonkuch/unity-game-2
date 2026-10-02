using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject plantPrefab;

    public PlantData sunflower;
    public PlantData shooter;
    public PlantData wall;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    Vector2Int? GetCurrentCell()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        mouseWorldPos.z = 0f; 

        Vector3Int cellCoordinate = GameGrid.Instance.tilemap.WorldToCell(mouseWorldPos)
                                    + new Vector3Int(1, 1, 0);

        if (cellCoordinate.x < 0 || cellCoordinate.y < 0)
            return null;
        
        if (cellCoordinate.x > GameGrid.Instance.Size.x - 1 || cellCoordinate.y > GameGrid.Instance.Size.y - 1)
            return null;

        return new Vector2Int(
            cellCoordinate.x,
            cellCoordinate.y
        );
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        var selection = GetCurrentCell();
        if (!selection.HasValue)
            return;
        
        bool occupied = GameGrid.Instance.Plants[selection.Value.x, selection.Value.y] != null;

        if (!occupied)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlacePlantAt(sunflower, selection.Value);    
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlacePlantAt(shooter, selection.Value);    
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlacePlantAt(wall, selection.Value);    
            }
        }
        
    }

    void PlacePlantAt(PlantData data, Vector2Int cellCoordinates)
    {
        if (GameState.Instance.playerCurrency < data.cost)
            return;
        
        var position = GameGrid.Instance.GetGlobalPosition(cellCoordinates);
        
        var instance = Instantiate(plantPrefab, position, Quaternion.identity);

        instance.GetComponent<Plant>().Init(data, cellCoordinates);
        
        GameState.Instance.playerCurrency -= data.cost;
    }
}
