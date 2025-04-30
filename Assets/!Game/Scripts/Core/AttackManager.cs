using UnityEngine;

public class AttackManager : MonoSingleton<AttackManager>
{
    public void HandleAttack(IDamageable damageable)
    {
        if (damageable == null)
        {
            Debug.LogWarning("No damageable object found.");
            return;
        }
        
        MonoBehaviour currentSelectable = SelectionManager.Instance.CurrentSelectable as MonoBehaviour;
        if (currentSelectable == null)
        {
            Debug.LogWarning("No selectable object found.");
            return;
        }

        IAttacker attacker = currentSelectable.GetComponent<IAttacker>();
        if (attacker == null)
        {
            Debug.LogWarning("No attacker selected.");
            return;
        }
        
        attacker.Attack(damageable);
    }
}
