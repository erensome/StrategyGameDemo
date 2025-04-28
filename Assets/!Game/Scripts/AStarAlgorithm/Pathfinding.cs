using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace AStarAlgorithm
{
    public class Pathfinding
    {
        private Grid<PathNode> grid;
        private HashSet<PathNode> openList;
        private HashSet<PathNode> closedList;
        
        public Grid<PathNode> Grid => grid;
        
        // Costs for movement
        private const int StraightCost = 10;
        private const int DiagonalCost = 14;

        public Pathfinding(int width, int height, float cellSize, Vector3 originPosition, bool showDebug = false)
        {
            grid = new Grid<PathNode>(width, height, cellSize, originPosition,
                (g, x, y) => new PathNode(g, x, y, true), showDebug);
        }
        
        public PathNode GetNode(int x, int y)
        {
            return grid.GetGridObject(x, y);
        }
        
        public List<Vector3> FindPath(Vector3 startPos, Vector3 endPos)
        {
            grid.GetXY(startPos, out int startX, out int startY);
            grid.GetXY(endPos, out int endX, out int endY);
            List<PathNode> pathList = FindPath(startX, startY, endX, endY);
            if (pathList == null) return null;
            
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in pathList)
            {
                vectorPath.Add(grid.GetWorldPosition(pathNode.x, pathNode.y) + Vector3.one * grid.CellSize * 0.5f);
            }
            
            return vectorPath;
        }

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            PathNode startNode = grid.GetGridObject(startX, startY);
            PathNode endNode = grid.GetGridObject(endX, endY);
            
            if (!endNode.IsWalkable || !startNode.IsWalkable)
            {
                Debug.LogWarning("Start or end node is not walkable");
                return null;
            }
            
            openList = new HashSet<PathNode>() {startNode};
            closedList = new HashSet<PathNode>();
            
            ResetGrid();
            
            startNode.gCost = 0;
            startNode.hCost = GetDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            while (openList.Count > 0)
            {
                PathNode currentNode = GetLowestFCostNode(openList);
                if (currentNode == endNode) // Reached the end node
                {
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
                {
                    if (closedList.Contains(neighbourNode)) continue; // Already evaluated
                    if (!neighbourNode.IsWalkable) // Not walkable
                    {
                        closedList.Add(neighbourNode);
                        continue;
                    }
                    
                    int tentativeGCost = currentNode.gCost + GetDistanceCost(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.gCost) // Found a better path
                    {
                        // Update the path node
                        neighbourNode.cameFromNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = GetDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        openList.Add(neighbourNode); // Add to open list if not already present
                    }
                }
            }

            // No path found
            return null;
        }

        private List<PathNode> GetNeighbourList(PathNode pathNode)
        {
            List<PathNode> neighbourList = new List<PathNode>();

            if (pathNode.x - 1 >= 0)
            {
                // Left
                neighbourList.Add(grid.GetGridObject(pathNode.x - 1, pathNode.y));
                // Bottom Left
                if (pathNode.y - 1 >= 0) neighbourList.Add(grid.GetGridObject(pathNode.x - 1, pathNode.y - 1));
                // Top Left
                if (pathNode.y + 1 >= 0) neighbourList.Add(grid.GetGridObject(pathNode.x - 1, pathNode.y + 1));
            }
            if (pathNode.x + 1 < grid.Width)
            {
                // Right
                neighbourList.Add(grid.GetGridObject(pathNode.x + 1, pathNode.y));
                // Bottom Right
                if (pathNode.y - 1 >= 0) neighbourList.Add(grid.GetGridObject(pathNode.x + 1, pathNode.y - 1));
                // Top Right
                if (pathNode.y + 1 < grid.Height) neighbourList.Add(grid.GetGridObject(pathNode.x + 1, pathNode.y + 1));
            }
            if (pathNode.y - 1 >= 0)
            {
                // Down
                neighbourList.Add(grid.GetGridObject(pathNode.x, pathNode.y - 1));
            }
            if (pathNode.y + 1 < grid.Height)
            {
                // Up
                neighbourList.Add(grid.GetGridObject(pathNode.x, pathNode.y + 1));
            }
            
            return neighbourList;
        }
        
        private List<PathNode> CalculatePath(PathNode endNode)
        {
            List<PathNode> path = new List<PathNode> { endNode };
            PathNode previousNode = endNode.cameFromNode;
            
            while (previousNode != null)
            {
                path.Add(previousNode);
                previousNode = previousNode.cameFromNode;
            }
            
            path.Reverse();
            return path;
        }

        private void ResetGrid()
        {
            for (int i = 0; i < grid.Width; i++)
            {
                for (int j = 0; j < grid.Height; j++)
                {
                    PathNode pathNode = grid.GetGridObject(i, j);
                    pathNode.gCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.cameFromNode = null;
                }
            }
        }
        
        /// <summary>
        /// Calculates the heuristic distance cost between two nodes.
        /// </summary>
        /// <param name="startNode">Start node</param>
        /// <param name="endNode">End node</param>
        /// <returns>hCost of start node</returns>
        private int GetDistanceCost(PathNode startNode, PathNode endNode)
        {
            int xDistance = Mathf.Abs(startNode.x - endNode.x);
            int yDistance = Mathf.Abs(startNode.y - endNode.y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            return DiagonalCost * Mathf.Min(xDistance, yDistance) + StraightCost * remaining;
        }

        private PathNode GetLowestFCostNode(HashSet<PathNode> pathNodeList)
        {
            PathNode lowestFCostNode = null;
            foreach (PathNode pathNode in pathNodeList)
            {
                if (lowestFCostNode == null || pathNode.fCost < lowestFCostNode.fCost)
                {
                    lowestFCostNode = pathNode;
                }
            }

            return lowestFCostNode;
        }
    }
}
