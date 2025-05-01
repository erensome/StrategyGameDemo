using UnityEngine;

namespace InputState
{
    /// <summary>
    /// Handles the input state when the player is in the building state.
    /// </summary>
    public class BuildingStateHandler : IInputStateHandler
    {
        public void HandleLeftClick(Vector3 mousePosition)
        {
            if (InputManager.Instance.IsMouseOverUI) return;
            
            // BuildingState'de iken sol tıklama yapılırsa building placement işlemi yapılır.
            BuildManager.Instance.HandleBuild();
        }

        public void HandleRightClick(Vector3 mousePosition) { }
    }
}
