using AStarAlgorithm;
using UnityEngine;
using GridSystem;
using Random = UnityEngine.Random;

/// <summary>
/// This class creates two grids. One for the ground and one for the pathfinding.
/// Ground grid is declared in this class.
/// Pathfinding grid is declared in the Pathfinding class.
/// </summary>
public class GroundManager : MonoSingleton<GroundManager>
{
    [Header("World Settings")]
    [SerializeField] private int width = 100;
    [SerializeField] private int height = 100;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private Transform worldOriginPoint;
    
    [Header("Ground Settings")]
    [SerializeField] private GroundCell cellPrefab; // whether ground or wall
    [SerializeField] private Sprite[] groundSprites;
    [SerializeField] private Sprite wallSprite;
    
    private Grid<GroundCell> grid;
    private Pathfinding pathfinding; // includes Grid<PathNode> grid
    public Pathfinding Pathfinding => pathfinding;
    
    private void Awake()
    {
        InitializePathfinding();
    }

    private void Start()
    {
        InitializeCells();
    }

    /// <summary>
    /// Initialize path nodes
    /// </summary>
    private void InitializePathfinding()
    {
        // Pathfinding creates a grid of PathNode objects
        pathfinding = new Pathfinding(
            Mathf.FloorToInt(width / cellSize),
            Mathf.FloorToInt(height / cellSize),
            cellSize,
            worldOriginPoint.position
        );
    }

    private void InitializeCells()
    {
        grid = new Grid<GroundCell>(
            Mathf.FloorToInt(width / cellSize),
            Mathf.FloorToInt(height / cellSize),
            cellSize,
            worldOriginPoint.position,
            (g, x, y) => CreateCell(x, y)
        );
    }
    
    private GroundCell CreateCell(int x, int y)
    {
        GroundCell cell = Instantiate(cellPrefab, worldOriginPoint.position, Quaternion.identity);
        cell.name = $"Cell_{x}_{y}";

        Transform cellTransform = cell.transform;
        cellTransform.SetParent(worldOriginPoint);
        cellTransform.localPosition = new Vector3(x * cellSize, y * cellSize , 0);
        
        Sprite sprite = DetermineSprite(x, y);
        cell.Initialize(grid, x, y, sprite, pathfinding.GetNode(x, y));

        return cell;
    }

    private Sprite DetermineSprite(int x, int y)
    {
        if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
        {
            return wallSprite;
        }
        
        return groundSprites[Random.Range(0, groundSprites.Length)];
    }
}
