using UnityEngine;

public interface IAttacker
{
    public GameObject AttackerObject { get; }
    float AttackDamage { get; }
    
    void Attack(IDamageable target);
    void StopAttack();    
}
