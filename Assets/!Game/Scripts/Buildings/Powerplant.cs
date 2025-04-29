using UnityEngine;
using Components;

[RequireComponent(typeof(EntityComponent))]
[RequireComponent(typeof(DamageableComponent))]
[RequireComponent(typeof(SelectableComponent))]
[RequireComponent(typeof(BlockerComponent))]
[RequireComponent(typeof(BuildingProduct))]
[RequireComponent(typeof(BuildableComponent))]
public class Powerplant : MonoBehaviour
{
    private BuildingData buildingData;
    
    [Header("Components")]
    [SerializeField] private EntityComponent entityComponent;
    [SerializeField] private DamageableComponent damageableComponent;
    [SerializeField] private SelectableComponent selectableComponent;
    [SerializeField] private BlockerComponent blockerComponent;
    [SerializeField] private BuildingProduct buildingProduct;
    [SerializeField] private BuildableComponent buildableComponent;

    private void Awake()
    {
        buildingData = (BuildingData)entityComponent.EntityData;
        damageableComponent.MaxHealth = buildingData.Health;
    }
}
