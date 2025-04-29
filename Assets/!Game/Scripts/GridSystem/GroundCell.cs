using UnityEngine;
using AStarAlgorithm;

namespace GridSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class GroundCell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer cellSprite;
        [SerializeField] private BoxCollider2D boxCollider;

        private Grid<GroundCell> grid; // reference to the grid
        private int x, y; // cell coordinates
        private PathNode pathNode; // A* pathfinding node reference
    
        public PathNode PathNode => pathNode;
    
        public void Initialize(Grid<GroundCell> grid, int x, int y, Sprite sprite, PathNode pathNode)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            cellSprite.sprite = sprite;
            this.pathNode = pathNode;
        }
    }
}
