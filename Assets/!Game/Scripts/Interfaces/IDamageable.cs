using UnityEngine;

public interface IDamageable 
{
    GameObject DamageableObject { get; }
    float Health { get; }
    float MaxHealth { get; }
    bool IsDead { get; }

    void TakeDamage(float damage);
    void Heal(float heal);
    void Die();
}
