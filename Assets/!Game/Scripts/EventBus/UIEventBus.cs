using System;
using UI;

namespace EventBus
{
    public static class UIEventBus
    {
        public static event Action<ProductionMenuItem> OnProductionMenuItemSelected;
    
        /// <summary>
        /// Triggers when a building is selected on Production Menu. Null means double clicked on the same object.
        /// </summary>
        public static void TriggerProductionMenuItemSelected(ProductionMenuItem productionMenuItem)
        {
            OnProductionMenuItemSelected?.Invoke(productionMenuItem);
        }
    }
}
