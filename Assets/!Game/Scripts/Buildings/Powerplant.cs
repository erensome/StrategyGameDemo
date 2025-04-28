using UnityEngine;
using Components;

[RequireComponent(typeof(EntityComponent))]
[RequireComponent(typeof(DamageableComponent))]
[RequireComponent(typeof(SelectableComponent))]
[RequireComponent(typeof(BlockerComponent))]
public class Powerplant : MonoBehaviour
{
    private BuildingData buildingData;
    
    [Header("Components")]
    [SerializeField] private EntityComponent entityComponent;
    [SerializeField] private DamageableComponent damageableComponent;
    [SerializeField] private SelectableComponent selectableComponent;
    [SerializeField] private BlockerComponent blockerComponent;

    private void Awake()
    {
        buildingData = (BuildingData)entityComponent.EntityData;
        damageableComponent.MaxHealth = buildingData.Health;
    }
}
