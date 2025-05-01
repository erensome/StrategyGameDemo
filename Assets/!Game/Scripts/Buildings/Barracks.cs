using System;
using System.Collections;
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
    public class Barracks : BaseBuilding
    {
        [Header("Barracks Settings")]
        [SerializeField] private float soldierProductionTime = 4f;
        [SerializeField] private Transform spawnPoint;

        private Coroutine productionCoroutine;
        private WaitForSeconds waitForProductionTime;
        private int soldierTypeCount;

        protected override void Awake()
        {
            base.Awake();
            soldierTypeCount = Enum.GetValues(typeof(SoldierType)).Length;
            waitForProductionTime = new WaitForSeconds(soldierProductionTime);
        }

        protected override void OnBuilt()
        {
            base.OnBuilt();

            if (productionCoroutine != null)
                StopCoroutine(productionCoroutine);

            productionCoroutine = StartCoroutine(ProduceUnit());
        }

        protected override void OnReturnedToPool()
        {
            base.OnReturnedToPool();
            if (productionCoroutine != null)
                StopCoroutine(productionCoroutine);
        }

        /// <summary>
        /// Coroutine to produce units at the barracks.
        /// Produces a random type of soldier at the spawn point every production time.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ProduceUnit()
        {
            while (true)
            {
                yield return waitForProductionTime;

                int randomType = UnityEngine.Random.Range(0, soldierTypeCount);
                
                if (GroundManager.Instance.IsWalkable(spawnPoint.position))
                {
                    SoldierFactory.Instance.Produce((SoldierType)randomType, spawnPoint.position);
                }
                else // if SpawnPoint is not available then select a randomly new spawn point
                {
                    spawnPoint.localPosition = UnityEngine.Random.onUnitSphere * buildingData.Size.x;
                    Debug.LogWarning("Spawn point is not available.");
                }
            }
        }
    }
}
