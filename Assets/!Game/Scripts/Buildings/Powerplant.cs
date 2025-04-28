using UnityEngine;

[RequireComponent(typeof(EntityComponent))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(SelectableComponent))]
[RequireComponent(typeof(BlockerComponent))]
public class Powerplant : MonoBehaviour
{
    private BuildingData buildingData;
    
    [Header("Components")]
    [SerializeField] private EntityComponent entityComponent;
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private SelectableComponent selectableComponent;
    [SerializeField] private BlockerComponent blockerComponent;

    private void Awake()
    {
        buildingData = (BuildingData)entityComponent.EntityData;
        healthComponent.MaxHealth = buildingData.Health;
    }
}
