using UnityEngine;

[RequireComponent(typeof(EntityComponent))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(SelectableComponent))]
public class Barracks : MonoBehaviour
{
    private BuildingData buildingData;
    
    [Header("Components")]
    [SerializeField] private EntityComponent entityComponent;
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private SelectableComponent selectableComponent;
    
    [Header("References")]
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        buildingData = (BuildingData)entityComponent.EntityData;
        healthComponent.MaxHealth = buildingData.Health;
    }
}
