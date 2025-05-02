using System.Collections.Generic;
using Components;
using UnityEngine;
using Factory;

namespace Buildings
{
    [RequireComponent(typeof(EntityComponent))]
    [RequireComponent(typeof(DamageableComponent))]
    [RequireComponent(typeof(SelectableComponent))]
    [RequireComponent(typeof(BuildingProduct))]
    [RequireComponent(typeof(BuildableComponent))]
    [RequireComponent(typeof(ProducerComponent))]
    public class Barracks : BaseBuilding
    {
        [SerializeField] private ProducerComponent producerComponent;
        
        [Header("Barracks Settings")]
        [SerializeField] private Transform spawnPoint;
        
        private List<Vector2> spawnPoints = new()
        {
            new Vector2(0, -2.5f),
            new Vector2(0, 2.5f),
            new Vector2(-2.5f, 0),
            new Vector2(2.5f, 0)
        };
        
        private Coroutine productionCoroutine;

        protected override void Awake()
        {
            base.Awake();
            producerComponent.OnProduced += HandleProduce;
        }

        protected override void OnBuilt()
        {
            base.OnBuilt();
            CheckSpawnPoint();
        }

        protected override void OnReturnedToPool()
        {
            base.OnReturnedToPool();
            if (productionCoroutine != null)
                StopCoroutine(productionCoroutine);
        }
        
        private void HandleProduce(EntityData entityData)
        {
            ProduceUnit(entityData);
        }

        /// <summary>
        /// Coroutine to produce units at the barracks.
        /// Produces a random type of soldier at the spawn point every production time.
        /// </summary>
        /// <returns></returns>
        private void ProduceUnit(EntityData entityData)
        {
            var selectedEntity = buildingData.ProductionItems.Find(i => i == entityData);
            SoldierUnitData soldierUnitData = selectedEntity as SoldierUnitData;
            if (soldierUnitData == null)
            {
                Debug.LogError("Selected entity is not a soldier unit data.");
                return;
            }
            
            if (GroundManager.Instance.IsWalkable(spawnPoint.position))
            {
                SoldierFactory.Instance.Produce(soldierUnitData.SoldierType, spawnPoint.position);
            }
            else // if SpawnPoint is not available then select a randomly new spawn point
            {
                Debug.LogError("Spawn point is not available.");
            }
        }

        private void CheckSpawnPoint()
        {
            foreach (var point in spawnPoints)
            {
                if (GroundManager.Instance.IsWalkable(point))
                {
                    spawnPoint.localPosition = point;
                    return;
                }
            }
            
            // If no spawn point is available, set the spawn point to the center of the building
            spawnPoint.localPosition = Vector2.zero;
        }
    }
}
