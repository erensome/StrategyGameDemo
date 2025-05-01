using System;
using UI;

namespace EventBus
{
    public static class UIEventBus
    {
        public static event Action<string> OnMessageRaised;
        
        public static event Action<ProductionMenuItem> OnProductionMenuItemSelected;
    
        /// <summary>
        /// Triggers when a building is selected on Production Menu. Null means double clicked on the same object.
        /// </summary>
        public static void TriggerProductionMenuItemSelected(ProductionMenuItem productionMenuItem)
        {
            OnProductionMenuItemSelected?.Invoke(productionMenuItem);
        }
        
        /// <summary>
        /// Triggers when a message is raised. This is used to display messages in the UI.
        /// </summary>
        /// <param name="message">The message will be displayed in the UI.</param>
        public static void TriggerMessageRaised(string message)
        {
            OnMessageRaised?.Invoke(message);
        }
    }
}
