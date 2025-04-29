using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class SoldierFactory : MonoSingleton<SoldierFactory>, IFactory<SoldierProduct, SoldierType>
    {
        [SerializeField] private List<SoldierUnitData> soldierUnits = new();
        
        public SoldierProduct Produce(SoldierType soldierType, Vector3 position)
        {
            foreach (var soldierUnit in soldierUnits)
            {
                if (soldierUnit.SoldierType == soldierType)
                {
                    GameObject soldier = ObjectPoolManager.Instance.GetObjectFromPool(soldierUnit.Name, position, Quaternion.identity);
                    return soldier.GetComponent<SoldierProduct>();
                }
            }

            Debug.LogError("Failed to produce soldier.");
            return null;
        }
    }
}
