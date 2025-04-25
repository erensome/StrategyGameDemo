using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] protected HealthBar healthBarPrefab;
    protected HealthBar healthBar;
    protected float health;
    protected float maxHealth;
    
    public float Health => health;
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(value, MinHealth);
    }
    public bool IsDead => health <= 0;
    
    /// <summary>
    /// Called when the object takes damage, old health and new health respectively.
    /// </summary>
    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;
    
    private const float MinHealth = 0f;

    protected virtual void Start()
    {
        health = maxHealth;
        
        if (healthBarPrefab != null)
        {
            healthBar = Instantiate(healthBarPrefab, transform);
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(maxHealth);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (IsDead) return;
        
        float oldHealth = health;
        health = Mathf.Clamp(health - damage, MinHealth, maxHealth);
        if (healthBar != null) healthBar.SetHealth(health);
        if (health <= MinHealth) Die();
        
        OnHealthChanged?.Invoke(oldHealth, health);
    }
    
    public virtual void Heal(float heal)
    {
        if (IsDead) return;
        
        float oldHealth = health;
        health = Mathf.Clamp(health + heal, MinHealth, maxHealth);
        if (healthBar != null) healthBar.SetHealth(health);
        
        OnHealthChanged?.Invoke(oldHealth, health);
    }
    
    public virtual void Die()
    {
        health = 0;
        OnDeath?.Invoke();
    }
    
    public virtual void ResetHealth()
    {
        health = maxHealth;
    }
}
