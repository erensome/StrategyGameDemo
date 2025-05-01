using System;
using UnityEngine;

namespace Components
{
    public class BuildableComponent : MonoBehaviour, IBuildable
    {
        public event Action OnBuild;
        public GameObject BuildableObject => gameObject;
        
        private BuildingData buildingData;
        private Vector2Int size;
        private float worldCellSize;
        
        private void Awake()
        {
            buildingData = GetComponent<EntityComponent>().EntityData as BuildingData;
            if (buildingData == null)
            {
                Debug.LogError("BuildingData is not assigned or is null.");
                return;
            }
        }
        
        private void Start()
        {
            size = buildingData.Size;
            worldCellSize = GroundManager.Instance.CellSize;
        }
        
        #region IBuildable
        public void Build()
        {
            BlockGrounds();
            transform.SetParent(null);
            OnBuild?.Invoke();
        }
        
        public void Remove()
        {
            UnblockGrounds();
        }
        #endregion
        
        private void BlockGrounds()
        {
            SetGroundsBlocked(false);
        }

        private void UnblockGrounds()
        {
            SetGroundsBlocked(true);
        }

        private void SetGroundsBlocked(bool isWalkable)
        {
            Vector2 sizeRatio = size / 2;
            Vector2 startPoints = new Vector2(-sizeRatio.x + worldCellSize / 2, -sizeRatio.y + worldCellSize / 2);

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    float x = startPoints.x + i * worldCellSize;
                    float y = startPoints.y + j * worldCellSize;
                    Vector3 position = new Vector3(x, y, 0) + transform.position; // world position of each cell
                    GroundManager.Instance.SetWalkableGround(position, isWalkable);
                }
            }
        }
    }
}
