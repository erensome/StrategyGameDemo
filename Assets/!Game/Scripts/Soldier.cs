using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Soldier : MonoBehaviour, IPoolable
{
    [SerializeField] private SoldierUnitData unitData;
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private Transform visualsParent;
    
    private void Awake()
    {
        if (healthComponent == null)
        {
            healthComponent = GetComponent<HealthComponent>();
        }
        
        healthComponent.MaxHealth = unitData.Health;
        healthComponent.OnHealthChanged += HandleChangeHealth;
        healthComponent.OnDeath += HandleDeath;
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
    
    private void HandleChangeHealth(float oldHealth, float newHealth)
    {
        Debug.Log($"Health changed from {oldHealth} to {newHealth}");
    }
    
    private void HandleDeath()
    {
        Debug.Log("Soldier died");
    }
}
