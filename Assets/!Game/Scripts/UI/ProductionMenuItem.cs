using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using EventBus;

namespace UI
{
    public class ProductionMenuItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image image;
        private BuildingData buildingData;
        private bool isFocused;

        public RectTransform RectTransform => rectTransform;
        public BuildingData BuildingData => buildingData;

        public void OnPointerClick(PointerEventData eventData)
        {
            UIEventBus.TriggerProductionMenuItemSelected(isFocused ? null : this);
        }

        public void Focus()
        {
            rectTransform.localScale = Vector3.one * 1.2f;
            isFocused = true;
        }
        
        public void Unfocus()
        {
            rectTransform.localScale = Vector3.one;
            isFocused = false;
        }

        public void Initialize(BuildingData buildingData)
        {
            this.buildingData = buildingData;
            image.sprite = buildingData.Icon;
        }
    }
}
