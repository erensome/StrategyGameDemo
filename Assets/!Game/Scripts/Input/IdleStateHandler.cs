using UnityEngine;

namespace InputState
{
    /// <summary>
    /// Handles the input state when the player is idle.
    /// </summary>
    public class IdleStateHandler : IInputStateHandler
    {
        public void HandleLeftClick(Vector3 mousePosition)
        {
            SelectionManager.Instance.HandleSelection(mousePosition);
        }

        public void HandleRightClick(Vector3 mousePosition) { }
    }
}


