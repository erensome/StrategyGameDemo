using GridSystem;
using UnityEngine;

namespace AStarAlgorithm
{
    public class PathNode
    {
        private Grid<PathNode> grid;
        private bool isWalkable;

        public int x, y;

        public int gCost; // Cost from start node to this node
        public int hCost; // Heuristic cost from this node to the end node
        public int fCost; // Total cost (gCost + hCost)
        public PathNode cameFromNode;

        public bool IsWalkable
        {
            get => isWalkable;
            set
            {
                isWalkable = value;
                grid.TriggerGridObjectChanged(x, y);
            }
        }
        
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
    }
}
