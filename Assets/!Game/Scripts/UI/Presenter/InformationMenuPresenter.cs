using System;
using EventBus;
using UnityEngine;

namespace UI
{
    public class InformationMenuPresenter : MonoBehaviour
    {
        [SerializeField] private InformationMenuView view;
        
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

        private void OnEntitySelected(EntityData entityData)
        {
            view.Display(entityData);
        }

        private void OnProductionMenuItemSelected(ProductionMenuItem productionMenuItem)
        {
            view.Hide();
        }
    }
}
