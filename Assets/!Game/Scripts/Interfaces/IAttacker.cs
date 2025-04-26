public interface IAttacker
{
    float AttackDamage { get; }
    
    void Attack(IDamageable target);
    void StopAttack();    
}
