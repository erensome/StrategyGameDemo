using UnityEngine;
using Components;

namespace Units
{
    /// <summary>
    /// This class is used to control the soldier unit.
    /// </summary>
    [RequireComponent(typeof(EntityComponent))]
    [RequireComponent(typeof(DamageableComponent))]
    [RequireComponent(typeof(AttackerComponent))]
    [RequireComponent(typeof(SelectableComponent))]
    [RequireComponent(typeof(MovableComponent))]
    [RequireComponent(typeof(SoldierProduct))]
    public class Soldier : MonoBehaviour
    {
        private SoldierUnitData unitData;

        [Header("Components")] [SerializeField]
        private EntityComponent entityComponent;

        [SerializeField] private DamageableComponent damageableComponent;
        [SerializeField] private AttackerComponent attackerComponent;
        [SerializeField] private SelectableComponent selectableComponent;
        [SerializeField] private MovableComponent movableComponent;
        [SerializeField] private SoldierProduct soldierProduct;

        [Header("References")] [SerializeField, Tooltip("Visual parent, using for look at rotations")]
        private Transform visualTransform;

        private void Awake()
        {
            unitData = (SoldierUnitData)entityComponent.EntityData;

            damageableComponent.MaxHealth = unitData.Health;
            attackerComponent.AttackDamage = unitData.Damage;

            damageableComponent.OnDeath += HandleDeath;
            soldierProduct.OnSpawned += HandleSpawn;
            soldierProduct.OnReturnedToPool += damageableComponent.ResetHealth;
            attackerComponent.OnAttack += HandleAttack;
            movableComponent.OnTargetPositionChanged += HandleTargetPositionChanged;
        }

        private void OnDestroy()
        {
            damageableComponent.OnDeath -= HandleDeath;
            soldierProduct.OnSpawned -= HandleSpawn;
            soldierProduct.OnReturnedToPool -= damageableComponent.ResetHealth;
            attackerComponent.OnAttack -= HandleAttack;
            movableComponent.OnTargetPositionChanged -= HandleTargetPositionChanged;
        }

        private void HandleSpawn()
        {
            damageableComponent.ResetHealth();
            damageableComponent.SetActiveHealthBar(true);
        }

        private void HandleDeath()
        {
            ObjectPoolManager.Instance.ReturnObjectToPool(unitData.Name, gameObject);
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
    }
}
