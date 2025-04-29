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
        }

        private void OnDestroy()
        {
            GameEventBus.OnEntitySelected -= OnEntitySelected;
        }

        private void OnEntitySelected(EntityData entityData)
        {
            view.Display(entityData);
        }
    }
}
