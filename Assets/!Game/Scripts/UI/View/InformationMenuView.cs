using System;
using System.Collections.Generic;
using EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InformationMenuView : MonoBehaviour
    {
        [Header("Entity Section")]
        [SerializeField] private GameObject entitySection;
        [SerializeField] private TextMeshProUGUI entityNameText;
        [SerializeField] private Image entityImage;
        
        [Header("Production Section")]
        [SerializeField] private GameObject productionSection;
        [SerializeField] private Image productImage;
        [SerializeField] private TextMeshProUGUI productNameText;
        [SerializeField] private Button previousProductButton;
        [SerializeField] private Button nextProductButton;

        private List<EntityData> productionItems = new();
        private int currentProductionIndex = 0;
        
        private void Start()
        {
            // Hide sections at the start
            Hide();
        }
        
        public void Hide()
        {
            entitySection.SetActive(false);
            productionSection.SetActive(false);
            productionItems.Clear();
        }
        
        public void Display(EntityData entityData)
        {
            // null means clicked on empty space
            if (entityData == null)
            {
                Hide();
                return;
            }
            
            entitySection.gameObject.SetActive(true);
            SetEntitySectionData(entityData);

            // The entity is a building and it has production items, then show the production section
            if (entityData is BuildingData buildingData && buildingData.ProductionItems.Count > 0)
            {
                productionSection.SetActive(true);
                SetProductionSectionData(buildingData);
            }
            else
            {
                productionSection.SetActive(false);
                productionItems.Clear();
            }
        }
        
        private void SetEntitySectionData(EntityData entityData)
        {
            entityNameText.text = entityData.Name;
            entityImage.sprite = entityData.Icon;
        }
        
        private void SetProductionSectionData(BuildingData buildingData)
        {
            productionItems.AddRange(buildingData.ProductionItems);
            currentProductionIndex = 0;
            SetProductInfo(productionItems[currentProductionIndex]);
            
            // Set buttons' interactable state
            previousProductButton.interactable = currentProductionIndex > 0;
            nextProductButton.interactable = currentProductionIndex < productionItems.Count - 1;
        }

        private void SetProductInfo(EntityData productEntityData)
        {
            productImage.sprite = productEntityData.Icon;
            productNameText.text = productEntityData.Name;
        }
        
        public void PreviousProductionItem()
        {
            SetProductInfo(productionItems[--currentProductionIndex]);

            if (currentProductionIndex == 0)
            {
                previousProductButton.interactable = false;   
            }
            
            nextProductButton.interactable = true;
        }
        
        public void NextProductionItem()
        {
            SetProductInfo(productionItems[++currentProductionIndex]);

            if (currentProductionIndex == productionItems.Count - 1)
            {
                nextProductButton.interactable = false;   
            }
            
            previousProductButton.interactable = true;
        }
    }
}
