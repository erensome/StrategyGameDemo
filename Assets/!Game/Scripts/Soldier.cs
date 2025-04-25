using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(AttackerComponent))]
[RequireComponent(typeof(SelectableComponent))]
public class Soldier : MonoBehaviour, IPoolable
{
    [Header("Data")]
    [SerializeField] private SoldierUnitData unitData;
    
    [Header("Components")]
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private AttackerComponent attackerComponent;
    [SerializeField] private SelectableComponent selectableComponent;
    
    private void Awake()
    {
        if (healthComponent == null)
        {
            healthComponent = GetComponent<HealthComponent>();
        }
        
        healthComponent.MaxHealth = unitData.Health;
    }
    
    #region IPoolable
    public void Spawn()
    {
        // Initialize the soldier if needed
    }

    public void ReturnToPool()
    {
        // Reset health and other properties if needed
        healthComponent.ResetHealth();
    }
    #endregion
}
