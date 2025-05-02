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
            view.Display(entityComponent);
            selectedEntityComponent = entityComponent;
        }

        private void OnProductionMenuItemSelected(ProductionMenuItem productionMenuItem)
        {
            view.Hide();
        }

        public void OnProduceButtonClicked()
        {
            selectedEntityComponent.gameObject.GetComponent<IProducer>().Produce(view.CurrentProductEntityData);
        }
    }
}
