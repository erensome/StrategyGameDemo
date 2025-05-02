using System;
using Components;

namespace EventBus
{
    public static class GameEventBus
    {
        public static event Action<EntityComponent> OnEntitySelected;
        public static event Action<IBuildable> OnBuildingPlaced;
        
        /// <summary>
        /// Triggers when an entity is selected in game board with the mouse.
        /// </summary>
        /// <param name="entityComponent">Selected entity's component.</param>
        public static void TriggerEntitySelected(EntityComponent entityComponent)
        {
            OnEntitySelected?.Invoke(entityComponent);
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
