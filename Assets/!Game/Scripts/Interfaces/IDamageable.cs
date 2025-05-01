using UnityEngine;

public interface IDamageable 
{
    GameObject DamageableObject { get; }
    float Health { get; }
    float MaxHealth { get; }
    bool IsDead { get; }

    /// <summary>
    /// Take damage from the object
    /// </summary>
    /// <param name="damage"></param>
    void TakeDamage(float damage);
    
    /// <summary>
    /// Heal the object
    /// </summary>
    /// <param name="heal"></param>
    void Heal(float heal);
    
    /// <summary>
    /// Die logic for the object
    /// </summary>
    void Die();
}
