using System;
using UnityEngine;
using UI;

namespace Components
{
    /// <summary>
    /// Component that handles the damageable logic for an entity.
    /// </summary>
    public class DamageableComponent : MonoBehaviour, IDamageable
    {
        private HealthBar healthBar;
        private float health;
        private float maxHealth;

        // Properties
        public GameObject DamageableObject => gameObject;
        public float Health => health;

        public float MaxHealth
        {
            get => maxHealth;
            set => maxHealth = Mathf.Max(value, MinHealth);
        }

        public bool IsDead { get; private set; }

        // Events
        /// <summary>
        /// Called when the object takes damage, old health and new health respectively.
        /// </summary>
        public event Action<float, float> OnHealthChanged;

        /// <summary>
        /// Called when the object dies.
        /// </summary>
        public event Action OnDeath;

        // Constants
        private const float MinHealth = 0f;

        protected virtual void Awake()
        {
            healthBar = ObjectPoolManager.Instance.GetObjectFromPool("HealthBar", transform).GetComponent<HealthBar>();
            SetActiveHealthBar(false);
        }
        
        public void SetActiveHealthBar(bool isActive)
        {
            if (healthBar != null)
            {
                healthBar.gameObject.SetActive(isActive);
            }
        }

        #region IDamageable

        public void TakeDamage(float damage)
        {
            if (IsDead) return;

            float oldHealth = health;
            health = Mathf.Clamp(health - damage, MinHealth, MaxHealth);
            OnHealthChanged?.Invoke(oldHealth, health);
            if (health <= MinHealth)
            {
                Die();
            }
        }

        public void Heal(float heal)
        {
            if (IsDead) return;

            float oldHealth = health;
            health = Mathf.Clamp(health + heal, MinHealth, MaxHealth);

            OnHealthChanged?.Invoke(oldHealth, health);
        }

        public void Die()
        {
            if (IsDead) return;

            IsDead = true;
            health = MinHealth;
            OnDeath?.Invoke();
        }

        public void ResetHealth()
        {
            if (IsDead) return;

            float oldHealth = health;
            health = MaxHealth;
            OnHealthChanged?.Invoke(oldHealth, health);
        }

        #endregion
    }
}
