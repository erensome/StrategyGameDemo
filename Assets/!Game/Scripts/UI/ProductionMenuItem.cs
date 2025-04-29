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

        public RectTransform RectTransform => rectTransform;

        public void OnPointerClick(PointerEventData eventData)
        {
            UIEventBus.TriggerOnBuildingSelected(buildingData);
        }

        public void Initialize(BuildingData buildingData)
        {
            this.buildingData = buildingData;
            image.sprite = buildingData.Icon;
        }
    }
}
