using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(AttackerComponent))]
[RequireComponent(typeof(SelectableComponent))]
[RequireComponent(typeof(MovableComponent))]
public class Soldier : MonoBehaviour, IPoolable
{
    [Header("Data")]
    [SerializeField] private SoldierUnitData unitData;
    
    [Header("Components")]
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private AttackerComponent attackerComponent;
    [SerializeField] private SelectableComponent selectableComponent;
    [SerializeField] private MovableComponent movableComponent;
    
    [Header("References")]
    [SerializeField, Tooltip("Visual parent, using for look at rotations")]
    private Transform visualTransform;
    
    private void Awake()
    {
        healthComponent.MaxHealth = unitData.Health;
        healthComponent.OnDeath += HandleDeath;
        
        attackerComponent.AttackDamage = unitData.Damage;
        attackerComponent.OnAttack += HandleAttack;
        movableComponent.OnTargetPositionChanged += HandleTargetPositionChanged;
    }

    private void HandleDeath()
    {
        ObjectPoolManager.Instance.ReturnObjectToPool(PoolType.Soldier, gameObject);
    }
    
    private void HandleAttack(IAttacker attacker, IDamageable target)
    {
        var targetObject = target as MonoBehaviour;
        if (targetObject == null) return;
        
        Vector3 targetPosition = targetObject.transform.position;
        Vector3 direction = targetPosition - transform.position;
        direction.z = 0;
        visualTransform.right = direction.normalized;
    }
    
    private void HandleTargetPositionChanged(Vector3 targetPosition)
    {
        visualTransform.right = (targetPosition - transform.position).normalized;
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
