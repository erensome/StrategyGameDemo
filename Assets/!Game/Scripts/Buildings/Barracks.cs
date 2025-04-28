using UnityEngine;
using Components;

[RequireComponent(typeof(EntityComponent))]
[RequireComponent(typeof(DamageableComponent))]
[RequireComponent(typeof(SelectableComponent))]
public class Barracks : MonoBehaviour
{
    private BuildingData buildingData;
    
    [Header("Components")]
    [SerializeField] private EntityComponent entityComponent;
    [SerializeField] private DamageableComponent damageableComponent;
    [SerializeField] private SelectableComponent selectableComponent;
    
    [Header("References")]
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        buildingData = (BuildingData)entityComponent.EntityData;
        damageableComponent.MaxHealth = buildingData.Health;
    }
}
