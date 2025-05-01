using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    /// <summary>
    /// Blueprint class for building system.
    /// It controls the blueprint grid and checks if the area is available for building.
    /// </summary>
    public class Blueprint : MonoBehaviour
    {
        private Camera mainCamera;
        private float cellSize;
        private bool isBlueprintActive;
        private readonly List<BlueprintGrid> blueprintGrids = new();
        
        private const string BlueprintGridPrefabName = "BlueprintGrid";
        private const float MinimumMovement = 0.0001f;
        
        public bool? IsAreaAvailable { get; private set; } // Nullable to indicate if the area is checked or not
        
        private void Awake()
        {
            cellSize = GroundManager.Instance.CellSize;
            mainCamera = Camera.main;
        }

        /// <summary>
        /// Mark as transparent when the building is in the blueprint state.
        /// mark as opaque when the building is placed.
        /// </summary>
        /// <param name="buildable"></param>
        /// <param name="transparent"></param>
        public void Mark(IBuildable buildable, bool transparent)
        {
            SpriteRenderer buildingSprite = buildable.BuildableObject.GetComponent<SpriteRenderer>();
            
            if (buildingSprite == null)
            {
                Debug.LogError("Building does not have a SpriteRenderer component.");
                return;
            }
            
            Color color = buildingSprite.color;
            color.a = transparent ? 0.5f : 1f;
            buildingSprite.color = color;
        }
        
        /// <summary>
        /// Moves the blueprint to the mouse position with a snap to grid effect.
        /// </summary>
        public void Move()
        {
            if (!isBlueprintActive) return;
            
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
    
            Vector3 position = new Vector3(
                CustomRound(mousePosition.x, cellSize),
                CustomRound(mousePosition.y, cellSize),
                0
            );

            // Check for minimal movement to avoid jittering
            if ((transform.position - position).sqrMagnitude > MinimumMovement)
            {
                transform.position = position;
            }
        }
        
        /// <summary>
        /// Checks if the area is available for building. If it is then colors the grid green, otherwise red.
        /// </summary>
        public void CheckGround()
        {
            if (!isBlueprintActive) return;
            
            bool isAvailable = true;
            foreach (var blueprintGrid in blueprintGrids)
            {
                Vector3 cellPosition = blueprintGrid.transform.position;
                if (!GroundManager.Instance.IsWalkable(cellPosition)) 
                {
                    isAvailable = false;
                    break;
                }
            }
            
            if (IsAreaAvailable != isAvailable) // Change the color only if the state changes
            {
                IsAreaAvailable = isAvailable;
                foreach (var blueprintGrid in blueprintGrids)
                {
                    blueprintGrid.Mark(isAvailable);
                }
            }
        }
        
        /// <summary>
        /// Creates the blueprint grid based on the given size and cell size.
        /// </summary>
        /// <param name="buildSize">Building size.</param>
        public void CreateBlueprint(Vector2Int buildSize)
        {
            float xStart = -(buildSize.x / 2) + 0.5f;
            float yStart = -(buildSize.y / 2) + 0.5f;
            
            for (int i = 0; i < buildSize.x; i++)
            {
                for (int j = 0; j < buildSize.y; j++)
                {
                    BlueprintGrid blueprintGrid =
                        ObjectPoolManager.Instance.GetObjectFromPool(BlueprintGridPrefabName, transform).GetComponent<BlueprintGrid>();
                    blueprintGrid.name = $"BlueprintGrid_{i}_{j}";
                    blueprintGrid.transform.localPosition = new Vector3(xStart + i * cellSize, yStart + j * cellSize, 0);
                    blueprintGrids.Add(blueprintGrid);
                }
            }
            
            isBlueprintActive = true;
        }
        
        /// <summary>
        /// Disposes the blueprint by destroying all the grid cells and resetting the state.
        /// </summary>
        public void DisposeBlueprint()
        {
            foreach (var cell in blueprintGrids)
            {
                Destroy(cell.gameObject);
            }
            
            blueprintGrids.Clear();
            isBlueprintActive = false;
            IsAreaAvailable = null;
        }
        
        /// <summary>
        /// Rounds the given float value to the nearest multiple of the specified step.
        /// </summary>
        /// <param name="value">The float value to be rounded.</param>
        /// <param name="step">The step size to which the value will be rounded. Must be non-zero.</param>
        /// <returns>
        /// The closest float that is a multiple of the step value.
        /// For example, CustomRound(2.3f, 0.5f) returns 2.5f.
        /// </returns>
        private float CustomRound(float value, float step)
        {
            if (step == 0f)
            {
                Debug.LogError("Step cannot be zero.");
                return value;
            }

            return Mathf.Round(value / step) * step;
        }
    }
}

