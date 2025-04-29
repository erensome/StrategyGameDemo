using System;

namespace EventBus
{
    public static class UIEventBus
    {
        public static event Action<BuildingData> OnBuildingSelected;
    
        /// <summary>
        /// Triggers when a building is selected on Production Menu.
        /// </summary>
        public static void TriggerOnBuildingSelected(BuildingData buildingData)
        {
            OnBuildingSelected?.Invoke(buildingData);
        }
    }
}
