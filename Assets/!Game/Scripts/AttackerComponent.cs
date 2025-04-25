using System;
using UnityEngine;

public class AttackerComponent : MonoBehaviour, IAttacker
{
    public float AttackDamage { get; }
    public float AttackRange { get; }
    public float AttackSpeed { get; }
    
    public event Action<IDamageable> OnAttack;
    
    public void Attack(IDamageable target)
    {
        
    }

    public void StopAttack()
    {
        
    }
}
