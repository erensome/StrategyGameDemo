using UnityEngine;
using Components;

[RequireComponent(typeof(EntityComponent))]
[RequireComponent(typeof(DamageableComponent))]
[RequireComponent(typeof(AttackerComponent))]
[RequireComponent(typeof(SelectableComponent))]
[RequireComponent(typeof(MovableComponent))]
public class Soldier : MonoBehaviour, IPoolable
{
    private SoldierUnitData unitData;
    
    [Header("Components")]
    [SerializeField] private EntityComponent entityComponent;
    [SerializeField] private DamageableComponent damageableComponent;
    [SerializeField] private AttackerComponent attackerComponent;
    [SerializeField] private SelectableComponent selectableComponent;
    [SerializeField] private MovableComponent movableComponent;
    
    [Header("References")]
    [SerializeField, Tooltip("Visual parent, using for look at rotations")]
    private Transform visualTransform;
    
    private void Awake()
    {
        unitData = (SoldierUnitData)entityComponent.EntityData;
        
        damageableComponent.MaxHealth = unitData.Health;
        damageableComponent.OnDeath += HandleDeath;
        
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
        LookAt(targetPosition);
    }
    
    private void HandleTargetPositionChanged(Vector3 targetPosition)
    {
        LookAt(targetPosition);
    }
    
    // I use Atan2 instead of transform.right because transform is not always right 
    private void LookAt(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        visualTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    
    #region IPoolable
    public void Spawn()
    {
        // Initialize the soldier if needed
    }

    public void ReturnToPool()
    {
        // Reset health and other properties if needed
        damageableComponent.ResetHealth();
    }
    #endregion
}
