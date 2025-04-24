using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar healthBarPrefab;
    private HealthBar healthBar;
    private float health;
    private float maxHealth;
    
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
    public Action<float, float> OnHealthChanged;
    public Action OnDeath;
    
    private const float MinHealth = 0f;

    private void Start()
    {
        health = maxHealth;
        
        if (healthBarPrefab != null)
        {
            healthBar = Instantiate(healthBarPrefab, transform);
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(maxHealth);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1f);
        }
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;
        
        float oldHealth = health;
        health = Mathf.Clamp(health - damage, MinHealth, maxHealth);
        healthBar.SetHealth(health);
        if (health <= MinHealth) Die();
        
        OnHealthChanged?.Invoke(oldHealth, health);
    }
    
    public void Heal(float heal)
    {
        if (IsDead) return;
        
        float oldHealth = health;
        health = Mathf.Clamp(health + heal, MinHealth, maxHealth);
        healthBar.SetHealth(health);
        OnHealthChanged?.Invoke(oldHealth, health);
    }
    
    public void Die()
    {
        health = 0;
        OnDeath?.Invoke();
    }
    
    public void ResetHealth()
    {
        health = maxHealth;
    }
}
