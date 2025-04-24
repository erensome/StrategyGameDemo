public interface IDamageable 
{
    float Health { get; }
    float MaxHealth { get; }

    void TakeDamage(float damage);
    void Heal(float heal);
    void Die();
}
