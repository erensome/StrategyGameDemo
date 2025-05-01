using UnityEngine;

public interface IAttacker
{
    GameObject AttackerObject { get; }
    float AttackDamage { get; }
    
    /// <summary>
    /// Method to attack a target
    /// </summary>
    /// <param name="target">Damageable target.</param>
    void Attack(IDamageable target);
}
