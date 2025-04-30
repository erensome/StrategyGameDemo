using UnityEngine;

namespace InputState
{
    public class IdleStateHandler : IInputStateHandler
    {
        // Idle'da iken sol tıklama yapılırsa selection olmuştur.
        public void HandleLeftClick(Vector3 mousePosition)
        {
            SelectionManager.Instance.HandleSelection(mousePosition);
        }

        public void HandleRightClick(Vector3 mousePosition) { }
    }
}


