using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private SelectionManager selectionManager;
    
    public void HandleAttack(Vector3 worldPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            IAttacker attacker = selectionManager.CurrentSelectable as IAttacker;
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            
            if (attacker == null)
            {
                Debug.LogWarning("No attacker selected.");
                return;
            }
            
            if (attacker != null && damageable != null)
            {
                attacker.Attack(damageable);
            }
        }
    }
}
