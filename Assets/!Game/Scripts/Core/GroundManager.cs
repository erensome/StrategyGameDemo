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
    
    [Header("Debug")]
    [SerializeField] private bool showDebugGrid = false;
    
    private Grid<GroundCell> grid;
    private Pathfinding pathfinding; // includes Grid<PathNode> grid
    private Vector2 visualOffset; // to center the cell sprites we should add visualOffset to the cell position
    
    public Pathfinding Pathfinding => pathfinding;
    public int Width => width;
    public int Height => height;
    public float CellSize => cellSize;
    public Vector2 WorldCenterPoint => worldOriginPoint.position + new Vector3(width * 0.5f, height * 0.5f) * cellSize;
    
    private void Awake()
    {
        visualOffset = Vector2.one * cellSize * 0.5f;
        InitializePathfinding();
        InitializeCells();
    }
    
    public void SetWalkableGround(Vector3 position, bool isWalkable)
    {
        grid.GetXY(position, out int x, out int y);
        SetWalkableGround(x, y, isWalkable);
    }
    
    public void SetWalkableGround(int x, int y, bool isWalkable)
    {
        GroundCell groundCell = grid.GetGridObject(x, y);
        groundCell.PathNode.IsWalkable = isWalkable;
    }
    
    public bool IsWalkable(Vector3 position)
    {
        GroundCell groundCell = grid.GetGridObject(position);
        if (groundCell == null) return false;
        return groundCell.PathNode.IsWalkable;
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
            worldOriginPoint.position,
            showDebugGrid
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
        GroundCell cell = ObjectPoolManager.Instance.GetObjectFromPool("GroundCell", worldOriginPoint.position,
            Quaternion.identity).GetComponent<GroundCell>();
        
        cell.name = $"Cell_{x}_{y}";

        Transform cellTransform = cell.transform;
        cellTransform.SetParent(worldOriginPoint);
        cellTransform.localPosition = new Vector2(x * cellSize, y * cellSize) + visualOffset;
        
        bool isWall = IsWall(x, y);
        Sprite sprite = SetSprite(isWall);
        cell.Initialize(grid, x, y, sprite, pathfinding.GetNode(x, y));
        cell.PathNode.IsWalkable = !isWall; // set walkable to false for the sides and corners
        return cell;
    }

    private Sprite SetSprite(bool isWall)
    {
        return isWall ? wallSprite : groundSprites[Random.Range(0, groundSprites.Length)];
    }
    
    // This method is used to check if the cell is a wall or not
    private bool IsWall(int x, int y)
    {
        return x == 0 || y == 0 || x == width - 1 || y == height - 1;
    }
}
