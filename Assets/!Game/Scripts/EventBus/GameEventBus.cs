using System;

namespace EventBus
{
    public static class GameEventBus
    {
        public static event Action<EntityData> OnEntitySelected;
    
        /// <summary>
        /// Triggers when an entity is selected in game board with the mouse.
        /// </summary>
        /// <param name="entityData">Selected entity's data.</param>
        public static void TriggerEntitySelected(EntityData entityData)
        {
            OnEntitySelected?.Invoke(entityData);
        }
    }
}
