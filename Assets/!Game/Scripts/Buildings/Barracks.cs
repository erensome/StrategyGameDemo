using UnityEngine;
using Components;

[RequireComponent(typeof(EntityComponent))]
[RequireComponent(typeof(DamageableComponent))]
[RequireComponent(typeof(SelectableComponent))]
[RequireComponent(typeof(BuildingProduct))]
[RequireComponent(typeof(BuildableComponent))]
public class Barracks : MonoBehaviour
{
    private BuildingData buildingData;
    
    [Header("Components")]
    [SerializeField] private EntityComponent entityComponent;
    [SerializeField] private DamageableComponent damageableComponent;
    [SerializeField] private SelectableComponent selectableComponent;
    [SerializeField] private BuildingProduct buildingProduct;
    [SerializeField] private BuildableComponent buildableComponent;
    
    [Header("References")]
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        buildingData = (BuildingData)entityComponent.EntityData;
        damageableComponent.MaxHealth = buildingData.Health;
    }
}
