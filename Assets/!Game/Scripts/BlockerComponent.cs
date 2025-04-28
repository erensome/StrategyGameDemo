using System;
using UnityEngine;

// This script set isWalkable to false for the grid node
public class BlockerComponent : MonoBehaviour
{
    private EntityComponent entityComponent;
    private Vector2Int size;

    private void Awake()
    {
        entityComponent = GetComponent<EntityComponent>();
    }

    private void Start()
    {
        size = entityComponent.EntityData.Size;
        BlockGrounds();
    }
    
    private void OnDisable()
    {
        if (GroundManager.Instance != null)
        {
            UnblockGrounds();
        }
    }

    private void SetGroundsBlocked(bool isWalkable)
    {
        float cellSize = GroundManager.Instance.CellSize;
        Vector2 sizeRatio = size / 2;
        Vector2 startPoints = new Vector2(-sizeRatio.x + cellSize / 2, -sizeRatio.y + cellSize / 2);

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                float x = startPoints.x + i * cellSize;
                float y = startPoints.y + j * cellSize;
                Vector3 position = new Vector3(x, y, 0) + transform.position; // world position of each cell
                GroundManager.Instance.SetWalkableGround(position, isWalkable);
            }
        }
    }

    private void BlockGrounds()
    {
        SetGroundsBlocked(false);
    }
    
    private void UnblockGrounds()
    {
        SetGroundsBlocked(true);
    }
}
