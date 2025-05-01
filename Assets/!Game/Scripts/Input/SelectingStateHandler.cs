using UnityEngine;

namespace InputState
{
    public class SelectingStateHandler : IInputStateHandler
    {
        // SelectingState'de iken sol tıklama yapılırsa başka seçme işlemi yapılmış olabilir.
        public void HandleLeftClick(Vector3 mousePosition)
        {
             SelectionManager.Instance.HandleSelection(mousePosition);
        }

        // SelectingState'de sağ tıklama yapıldıysa ya move yapılacaktır ya da attack yapılacaktır.
        public void HandleRightClick(Vector3 mousePosition)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            
            if (hit.collider != null && hit.collider.TryGetComponent(out IDamageable damageable))
            {
                AttackManager.Instance.HandleAttack(damageable);
            }
            else
            {
                MoveManager.Instance.HandleMove(mousePosition);
            }
        }
    }
}
