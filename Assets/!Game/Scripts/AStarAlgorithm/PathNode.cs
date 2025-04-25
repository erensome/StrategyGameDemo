using GridSystem;
using UnityEngine;

namespace AStarAlgorithm
{
    public class PathNode
    {
        private Grid<PathNode> grid;
        public int x, y;

        public int gCost; // Cost from start node to this node
        public int hCost; // Heuristic cost from this node to the end node
        public int fCost; // Total cost (gCost + hCost)

        public bool isWalkable;
        public PathNode cameFromNode;
        
        public PathNode(Grid<PathNode> grid, int x, int y, bool isWalkable)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            this.isWalkable = isWalkable;
        }

        public override string ToString()
        {
            return $"{x}, {y}";
        }
        
        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
        
        public void SetWalkable(bool isWalkable)
        {
            this.isWalkable = isWalkable;
            grid.TriggerGridObjectChanged(x, y);
        }
    }
}
