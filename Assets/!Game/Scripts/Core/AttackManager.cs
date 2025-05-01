using UnityEngine;

public class AttackManager : MonoSingleton<AttackManager>
{
    private IAttacker lastAttacker;
    private IDamageable lastDamageable;
    
    public void HandleAttack(IDamageable damageable)
    {
        if (damageable == null)
        {
            Debug.LogWarning("No damageable object found.");
            return;
        }

        ISelectable currentSelectable = SelectionManager.Instance.CurrentSelectable;
        IAttacker attacker = currentSelectable.SelectableObject.GetComponent<IAttacker>();
        if (attacker == null)
        {
            Debug.LogWarning("No attacker selected.");
            return;
        }

        if (attacker.AttackerObject == damageable.DamageableObject)
        {
            Debug.LogWarning("Cannot attack self.");
            return;
        }
        
        // Prevent attack spam on the same target
        if (lastAttacker == attacker && lastDamageable == damageable)
        {
            Debug.LogWarning("Already attacking this target.");
            return;
        }
        
        lastAttacker = attacker;
        lastDamageable = damageable;
        attacker.Attack(damageable);
    }
}
