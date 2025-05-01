using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class BuildingFactory : MonoSingleton<BuildingFactory>, IFactory<BuildingProduct, BuildingType>
    {
        [SerializeField] private List<BuildingData> buildings = new();
        
        public BuildingProduct Produce(BuildingType type, Vector2 position)
        {
            foreach (var building in buildings)
            {
                if (building.BuildingType == type)
                {
                    GameObject buildingObject = ObjectPoolManager.Instance.GetObjectFromPool(building.Name, position, Quaternion.identity);
                    return buildingObject.GetComponent<BuildingProduct>();
                }
            }
            
            Debug.LogError("Failed to produce building.");
            return null;
        }
    }
}
