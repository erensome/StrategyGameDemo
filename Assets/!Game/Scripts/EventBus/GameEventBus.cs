using System;

namespace EventBus
{
    public static class GameEventBus
    {
        public static event Action<EntityData> OnEntitySelected;
        public static event Action<IBuildable> OnBuildingPlaced;
        
        /// <summary>
        /// Triggers when an entity is selected in game board with the mouse.
        /// </summary>
        /// <param name="entityData">Selected entity's data.</param>
        public static void TriggerEntitySelected(EntityData entityData)
        {
            OnEntitySelected?.Invoke(entityData);
        }
        
        /// <summary>
        /// Triggers when a building is placed in game board with the mouse.
        /// </summary>
        /// <param name="buildableComponent"></param>
        public static void TriggerBuildingPlaced(IBuildable buildableComponent)
        {
            OnBuildingPlaced?.Invoke(buildableComponent);
        }
    }
}
