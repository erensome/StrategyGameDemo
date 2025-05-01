using UnityEngine;

namespace InputState
{
    public interface IInputStateHandler
    {
        /// <summary>
        /// Handles the left click input.
        /// </summary>
        /// <param name="mousePosition"></param>
        void HandleLeftClick(Vector3 mousePosition);
        
        /// <summary>
        /// Handles the right click input.
        /// </summary>
        /// <param name="mousePosition"></param>
        void HandleRightClick(Vector3 mousePosition);
    }
}
