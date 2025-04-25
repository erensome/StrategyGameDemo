public interface IAttacker
{
    float AttackDamage { get; }
    float AttackRange { get; }
    float AttackSpeed { get; }
    
    void Attack(IDamageable target);
    void StopAttack();    
}
