using System.Collections.Generic;
using AStarAlgorithm;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Pathfinding pathfinding;
    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        pathfinding = new Pathfinding(10, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            pathfinding.Grid.GetXY(mouseWorldPos, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path == null)
            {
                Debug.Log("No path found");
                return;
            }
            
            foreach (PathNode node in path)
            {
                Debug.Log(node.ToString());
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            pathfinding.Grid.GetXY(mouseWorldPos, out int x, out int y);
            PathNode pathNode = pathfinding.GetNode(x, y);
            pathNode.SetWalkable(!pathNode.isWalkable);
            
            Debug.Log($"Node at ({x}, {y}) is now {(pathNode.isWalkable ? "walkable" : "not walkable")}");
        }
    }
}
