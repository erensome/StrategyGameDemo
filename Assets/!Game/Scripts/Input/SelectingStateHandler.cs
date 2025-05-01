using UnityEngine;

namespace InputState
{
    /// <summary>
    /// Handles the input state when the player is in the selecting state.
    /// </summary>
    public class SelectingStateHandler : IInputStateHandler
    {
        public void HandleLeftClick(Vector3 mousePosition)
        {
             SelectionManager.Instance.HandleSelection(mousePosition);
        }

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
