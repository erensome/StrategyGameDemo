using UnityEngine;

namespace InputState
{
    public interface IInputStateHandler
    {
        void HandleLeftClick(Vector3 mousePosition);
        void HandleRightClick(Vector3 mousePosition);
    }
}
