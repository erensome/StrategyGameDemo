using System.Collections.Generic;
using Components;
using EventBus;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Presenter for the information menu
    /// </summary>
    public class InformationMenuPresenter : MonoBehaviour
    {
        [SerializeField] private InformationMenuView view;
        
        private EntityComponent selectedEntityComponent;
        private List<EntityData> productionItems = new();
        private int currentProductionIndex = 0;
        
        // Start is called before the first frame update
        void Start()
        {
            GameEventBus.OnEntitySelected += OnEntitySelected;
            UIEventBus.OnProductionMenuItemSelected += OnProductionMenuItemSelected;
        }

        private void OnDestroy()
        {
            GameEventBus.OnEntitySelected -= OnEntitySelected;
            UIEventBus.OnProductionMenuItemSelected -= OnProductionMenuItemSelected;
        }

        private void OnEntitySelected(EntityComponent entityComponent)
        {
            selectedEntityComponent = entityComponent;

            if (entityComponent == null || entityComponent.EntityData == null)
            {
                view.Hide();
                return;
            }

            view.DisplayEntity(entityComponent.EntityData);

            if (entityComponent.EntityData is BuildingData buildingData && buildingData.ProductionItems.Count > 0)
            {
                productionItems = new List<EntityData>(buildingData.ProductionItems);
                currentProductionIndex = 0;
                view.DisplayProduct(productionItems[0]);
                UpdateNavigationButtons();
            }
            else
            {
                productionItems.Clear();
                view.HideProductionSection();
            }
        }

        private void OnProductionMenuItemSelected(ProductionMenuItem productionMenuItem)
        {
            view.Hide();
        }

        public void OnProduceButtonClicked()
        {
            if (selectedEntityComponent != null &&
                selectedEntityComponent.TryGetComponent<IProducer>(out var producer))
            {
                producer.Produce(productionItems[currentProductionIndex]);
            }
        }
        
        public void OnNextProductButtonClicked()
        {
            if (currentProductionIndex < productionItems.Count - 1)
            {
                currentProductionIndex++;
                view.DisplayProduct(productionItems[currentProductionIndex]);
                UpdateNavigationButtons();
            }
        }

        public void OnPreviousProductButtonClicked()
        {
            if (currentProductionIndex > 0)
            {
                currentProductionIndex--;
                view.DisplayProduct(productionItems[currentProductionIndex]);
                UpdateNavigationButtons();
            }
        }

        private void UpdateNavigationButtons()
        {
            view.SetProductNavigationInteractable(
                previousEnabled: currentProductionIndex > 0,
                nextEnabled: currentProductionIndex < productionItems.Count - 1
            );
        }
    }
}
