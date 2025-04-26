using UnityEngine;

public class AttackManager : MonoSingleton<AttackManager>
{
    public void HandleAttack(Vector3 worldPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            MonoBehaviour currentSelectable = SelectionManager.Instance.CurrentSelectable as MonoBehaviour;
            if (currentSelectable == null)
            {
                Debug.LogWarning("No selectable object found.");
                return;
            }
            
            IAttacker attacker = currentSelectable.GetComponent<IAttacker>();
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            
            if (attacker == null)
            {
                Debug.LogWarning("No attacker selected.");
                return;
            }

            if (damageable == null)
            {
                Debug.LogWarning("No damageable object found.");
                return;
            }
            
            attacker.Attack(damageable);
        }
    }
}
