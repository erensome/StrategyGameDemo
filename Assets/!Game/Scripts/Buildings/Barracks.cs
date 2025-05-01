using System;
using System.Collections;
using UnityEngine;
using Components;
using DG.Tweening;
using Factory;
using Random = UnityEngine.Random;

[RequireComponent(typeof(EntityComponent))]
[RequireComponent(typeof(DamageableComponent))]
[RequireComponent(typeof(SelectableComponent))]
[RequireComponent(typeof(BuildingProduct))]
[RequireComponent(typeof(BuildableComponent))]
public class Barracks : MonoBehaviour
{
    [SerializeField] private float soldierProductionTime = 4f;
    private BuildingData buildingData;
    
    [Header("Components")]
    [SerializeField] private EntityComponent entityComponent;
    [SerializeField] private DamageableComponent damageableComponent;
    [SerializeField] private SelectableComponent selectableComponent;
    [SerializeField] private BuildingProduct buildingProduct;
    [SerializeField] private BuildableComponent buildableComponent;
    
    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    private SpriteRenderer spriteRenderer;

    private Coroutine productionCoroutine;
    private WaitForSeconds waitForProductionTime;
    private int soldierTypeCount;
    
    private void Awake()
    {
        buildingData = (BuildingData)entityComponent.EntityData;
        damageableComponent.MaxHealth = buildingData.Health;
        damageableComponent.OnDeath += HandleBarracksDestroy;
        buildableComponent.OnBuild += HandleBarracksBuild;
        buildingProduct.OnReturnedToPool += StopProduction;
        
        soldierTypeCount = Enum.GetValues(typeof(SoldierType)).Length;
        waitForProductionTime = new WaitForSeconds(soldierProductionTime);
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        damageableComponent.OnDeath -= HandleBarracksDestroy;
        buildingProduct.OnReturnedToPool -= StopProduction;
        buildableComponent.OnBuild -= HandleBarracksBuild;
    }
    
    private void HandleBarracksBuild()
    {
        damageableComponent.SetActiveHealthBar(true);
        
        if (productionCoroutine != null)
        {
            StopCoroutine(productionCoroutine);
        }
        
        productionCoroutine = StartCoroutine(ProduceUnit());
    }
    
    private void HandleBarracksDestroy()
    {
        spriteRenderer.DOFade(0, 0.6f).OnComplete(() =>
        {
            buildableComponent.Remove();
            ObjectPoolManager.Instance.ReturnObjectToPool(buildingData.Name, gameObject);
        });
    }
    
    private IEnumerator ProduceUnit()
    {
        while (true)
        {
            yield return waitForProductionTime;
        
            int randomSoldierType = Random.Range(0, soldierTypeCount);
            bool isSpawnAvailable = CheckSpawnPointAvailability();
            
            if (isSpawnAvailable)
            {
                SoldierProduct soldier = SoldierFactory.Instance.Produce((SoldierType)randomSoldierType, spawnPoint.position);
                soldier.transform.SetParent(null);
            }
            else
            {
                ChangeSpawnPointRandomly();
                Debug.LogWarning("Spawn point is not available.");
            }
        }
    }

    private void ChangeSpawnPointRandomly()
    {
        spawnPoint.localPosition = Random.onUnitSphere * buildingData.Size.x;
    }
    
    private bool CheckSpawnPointAvailability()
    {
        return GroundManager.Instance.IsWalkable(spawnPoint.position);
    }
    
    private void StopProduction()
    {
        if (productionCoroutine != null)
        {
            StopCoroutine(productionCoroutine);
        }
    }
}
