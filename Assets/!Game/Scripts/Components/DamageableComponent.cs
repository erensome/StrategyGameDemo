using System;
using UnityEngine;

namespace Components
{
    public class DamageableComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] private HealthBar healthBarPrefab;

        // Fields
        private float health;
        private float maxHealth;

        // Properties
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

        protected virtual void Start()
        {
            health = maxHealth;

            if (healthBarPrefab != null)
            {
                Instantiate(healthBarPrefab, transform);
            }
        }

        #region Virtual Methods

        protected virtual void AfterTakeDamage()
        {
        }

        protected virtual void AfterHeal()
        {
        }

        protected virtual void AfterDie()
        {
        }

        protected virtual void AfterReset()
        {
        }

        #endregion

        #region Template Methods

        public void TakeDamage(float damage)
        {
            if (IsDead) return;

            float oldHealth = health;
            health = Mathf.Clamp(health - damage, MinHealth, MaxHealth);
            AfterTakeDamage();
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

            AfterHeal();
            OnHealthChanged?.Invoke(oldHealth, health);
        }

        public void Die()
        {
            if (IsDead) return;

            IsDead = true;
            health = MinHealth;
            AfterDie();
            OnDeath?.Invoke();
        }

        public void ResetHealth()
        {
            if (IsDead) return;

            float oldHealth = health;
            health = MaxHealth;
            AfterReset();
            OnHealthChanged?.Invoke(oldHealth, health);
        }

        #endregion
    }
}
